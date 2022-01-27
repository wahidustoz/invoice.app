using System.ComponentModel.DataAnnotations.Schema;

namespace invoice.app.Entity;

public class InvoiceItem
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }

    public int Quantity { get; set; }
    
    public decimal Rate { get; set; }
    
    public decimal TaxRate { get; set; }

    public Guid InvoiceId { get; set; }
    public virtual Invoice Invoice { get; set; }
}