using System.ComponentModel.DataAnnotations.Schema;

namespace invoice.app.Entity;

public class Organization
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Phone { get; set; }
    
    public string Email { get; set; }

    public string Address { get; set; }

    public Guid OwnerId { get; set; }

    public virtual ICollection<EmployeeOrganization> EmployeeOrganizations { get; set; }
    
    public virtual ICollection<Invoice> Invoices { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; }
}