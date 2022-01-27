using System.ComponentModel.DataAnnotations.Schema;

namespace invoice.app.Entity;

public class Invoice
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string InvoiceNumber { get; set; }

    public string Subject { get; set; }
    
    public decimal Total { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset ModifiedAt { get; set; }

    public DateTimeOffset DueBy { get; set; }
    
    public DateTimeOffset SendEmailAt { get; set; }
    
    public EInvoiceState State { get; set; }

    public Guid OrganizationId { get; set; }
    public virtual Organization Organization { get; set; }
    
    public Guid? CreatorId { get; set; }
    public virtual AppUser Creator { get; set; }
    
    public virtual Partner BillTo { get; set; }

    public virtual ICollection<InvoiceItem> Items { get; set; }
}