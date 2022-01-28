using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace invoice.app.ViewModels;

public class AddMemberViewModel
{
    [Required(ErrorMessage = "Email manzilini kiritish shart!")]
    [EmailAddress(ErrorMessage = "Email manzil formati noto'g'ri.")]
    [DisplayName("Email manzili")]
    public string ForEmail { get; set; }

    [Required]
    public Guid OrganizationId { get; set; }
}