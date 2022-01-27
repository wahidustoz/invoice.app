using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace invoice.app.ViewModels;

public class SigninViewModel
{
    [Required(ErrorMessage = "Email manzil kiritish shart!")]
    [EmailAddress(ErrorMessage = "Email manzil formati noto'g'ri.")]
    [DisplayName("Email manzil")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Parolni kiritish shart!")]
    [MinLength(6, ErrorMessage = "Parol kamida 6 ta belgidan iborat bo'lishi kerak.")]
    [DisplayName("Parol")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
}