using invoice.app.Data;
using invoice.app.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class Seed : BackgroundService
{
    private UserManager<AppUser> _userM;
    private RoleManager<AppRole> _roleM;
    private readonly IServiceProvider _provider;
    private readonly ILogger<Seed> _logger;

    public Seed(
        IServiceProvider provider,
        ILogger<Seed> logger)
    {
        _provider = provider;
        _logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _provider.CreateScope();
        _userM = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        _roleM = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
        
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var conf = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var migrateDb = conf.GetValue<bool>("MigrateDatabase");
        
        _logger.LogInformation($"Database Migration option: {migrateDb}");
        if(migrateDb)
        {
            
            _logger.LogInformation($"Migrating Database...");
            ctx.Database.Migrate();
        }

        var roles = new []{ "superadmin", "owner", "employee" };
        foreach(var role in roles)
        {
            if(!await _roleM.RoleExistsAsync(role))
            {
                await _roleM.CreateAsync(new AppRole(role));
            }
        }

        if((await _userM.FindByEmailAsync("superadmin@ilmhub.uz")) == null)
        {
            var user = new AppUser()
            {
                Email = "superadmin@ilmhub.uz",
                PhoneNumber = "998950136172",
                UserName = "superadmin",
                Fullname = "Supar Admin"
            };

            var result = await _userM.CreateAsync(user, "123456");
            if(result.Succeeded)
            {
                var newUser = await _userM.FindByEmailAsync("superadmin@ilmhub.uz");
                await _userM.AddToRolesAsync(newUser, new [] { "superadmin", "employee", "owner" });
            }
        }

        var admin = await _userM.FindByEmailAsync("superadmin@ilmhub.uz");
        var organization = new Organization()
        {
            Id = Guid.NewGuid(),
            Name = "Ilmhub IT Schoool",
            Phone = "998950126172",
            Email = "ilmhub.uz@gmail.com",
            Address = "Buyuk Ipak Yuli street, 254",
            OwnerId = admin.Id
        };

        var employeeOrganization = new EmployeeOrganization()
        {
            EmployeeId = admin.Id,
            OrganizationId = organization.Id
        };

        var joinCode = new JoinCode()
        {
            ForEmail = "wakhid2802@gmail.com",
            CreatorId = admin.Id,
            OrganizationId = organization.Id
        };

        await ctx.Organizations.AddAsync(organization);
        await ctx.EmployeeOrganizations.AddAsync(employeeOrganization);
        await ctx.JoinCodes.AddAsync(joinCode);
        await ctx.SaveChangesAsync();
    }
}