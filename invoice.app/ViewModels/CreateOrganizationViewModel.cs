using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace invoice.app.ViewModels;

public class CreateOrganizationViewModel
{
    [Required(ErrorMessage = "To'liq nomni kiritish shart!")]
    [Display(Name = "Korxona nomi")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Email manzilini kiritish shart!")]
    [EmailAddress(ErrorMessage = "Email manzil formati noto'g'ri.")]
    [DisplayName("Email manzili")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Telefon raqam kiritish shart!")]
    [RegularExpression(
        @"^[\+]?(998[-\s\.]?)([0-9]{2}[-\s\.]?)([0-9]{3}[-\s\.]?)([0-9]{2}[-\s\.]?)([0-9]{2}[-\s\.]?)$", 
        ErrorMessage = "Telefon raqam formati noto'g'ri.")]
    [DisplayName("Telefon raqami")]
    public string Phone { get; set; }
    
    [Required(ErrorMessage = "Addressni kiritish!")]
    [MinLength(10, ErrorMessage = "Addressni to'liq kiritish shart.")]
    [DisplayName("Korxona manzili")]
    public string Address { get; set; }
    
    [DisplayName("Korxona haqida")]
    public string Description { get; set; }
}