using Microsoft.AspNetCore.Identity;

namespace invoice.app.Entity;

public class AppUserRole : IdentityUserRole<Guid>
{
    public virtual AppUser User { get; set; }
    
    public virtual AppRole Role{ get; set; }
}