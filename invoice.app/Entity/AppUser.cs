using Microsoft.AspNetCore.Identity;

namespace invoice.app.Entity;

public class AppUser : IdentityUser<Guid>
{
    public string Fullname { get; set; }
    
    public virtual ICollection<AppUserRole> UserRoles { get; set; }

    public virtual ICollection<EmployeeOrganization> EmployeeOrganizations { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; }
}