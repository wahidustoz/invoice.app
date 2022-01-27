using invoice.app.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace invoice.app.Data;

public class AppDbContext : IdentityDbContext<
                                            AppUser, 
                                            AppRole, 
                                            Guid, 
                                            IdentityUserClaim<Guid>, 
                                            AppUserRole, 
                                            IdentityUserLogin<Guid>, 
                                            IdentityRoleClaim<Guid>, 
                                            IdentityUserToken<Guid>>
{
    public DbSet<Organization> Organizations { get; set; }

    public DbSet<EmployeeOrganization> EmployeeOrganizations { get; set; }
    
    public DbSet<Invoice> Invoices { get; set; }

    public DbSet<InvoiceItem> IvoiceItems { get; set; }
    
    public DbSet<Partner> Partners { get; set; }

    public DbSet<JoinCode> JoinCodes { get; set; }
    

    public AppDbContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUserRole>(a =>
        {
            a.HasKey(r => new { r.UserId, r.RoleId });

            a.HasOne(r => r.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(r => r.UserId)
                .IsRequired();

            a.HasOne(r => r.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(r => r.RoleId)
                .IsRequired();  
        });

        builder.Entity<EmployeeOrganization>(eo =>
        {
            eo.HasKey(k => new { k.EmployeeId, k.OrganizationId });

            eo.HasOne(e => e.Employee)
                .WithMany(emo => emo.EmployeeOrganizations)
                .HasForeignKey(fk => fk.EmployeeId)
                .IsRequired();

            eo.HasOne(e => e.Organization)
                .WithMany(emo => emo.EmployeeOrganizations)
                .HasForeignKey(fk => fk.OrganizationId)
                .IsRequired();
        });

        builder.Entity<AppUser>(au => 
        {
            au.HasMany(u => u.Invoices)
                .WithOne(i => i.Creator)
                .HasForeignKey(i => i.CreatorId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<Organization>(o =>
        {
            o.HasMany(org => org.Invoices)
                .WithOne(i => i.Organization)
                .HasForeignKey(i => i.OrganizationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            o.HasMany(org => org.Partners)
                .WithOne(p => p.Organization)
                .HasForeignKey(p => p.OrganizationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Invoice>(i =>
        {
            i.HasMany(inv => inv.Items)
                .WithOne(item => item.Invoice)
                .HasForeignKey(item => item.InvoiceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            
            i.HasIndex(inv => new { inv.OrganizationId, inv.InvoiceNumber })
                .IsUnique();
        });

        builder.Entity<JoinCode>().HasKey(k => new { k.Code, k.ForEmail });
    }
}