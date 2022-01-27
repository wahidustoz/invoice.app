namespace invoice.app.Entity;

public class EmployeeOrganization
{
    public Guid EmployeeId { get; set; }
    
    public virtual AppUser Employee { get; set; }
    
    public Guid OrganizationId { get; set; }
    
    public virtual Organization Organization { get; set; }
}