using System.ComponentModel.DataAnnotations;

namespace invoice.app.ViewModels;

public class SignupViewModel
{
    public string ReturnUrl { get; set; }
    
    public Guid? JoinCode { get; set; }
    
    [Required]
    public string Fullname { get; set; }
    
    [Required]
    [RegularExpression(@"^[\+]?(998[-\s\.]?)([0-9]{2}[-\s\.]?)([0-9]{3}[-\s\.]?)([0-9]{2}[-\s\.]?)([0-9]{2}[-\s\.]?)$")]
    public string Phone { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
    
    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
    
    
}