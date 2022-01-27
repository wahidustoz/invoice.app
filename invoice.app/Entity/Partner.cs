using System.ComponentModel.DataAnnotations.Schema;

namespace invoice.app.Entity;

public class Partner
{
    public Guid Id { get; set; }
    
    public string Email { get; set; }
    
    public string Phone { get; set; }
    
    public string Address { get; set; }

    public Guid OrganizationId { get; set; }
    public virtual Organization Organization { get; set; }
}