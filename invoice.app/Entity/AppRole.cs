using Microsoft.AspNetCore.Identity;

namespace invoice.app.Entity;

public class AppRole : IdentityRole<Guid>
{   
    [Obsolete("", true)]
    public AppRole() { }
    public AppRole(string role) : base(role) { }

    public virtual ICollection<AppUserRole> UserRoles { get; set; }
}