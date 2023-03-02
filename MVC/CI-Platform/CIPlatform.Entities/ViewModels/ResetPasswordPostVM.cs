using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class ResetPasswordPostVM: PasswordResetVM
{
    [Required(ErrorMessage = "Password cannot be empty!")]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must have at least 8 character!")]
    public string Password { get; set; } = String.Empty;


    [Required(ErrorMessage = "Confirm password must be same as password!")]
    [Compare("Password")]
    [DisplayName("Confirm Password")]
    public string ConfirmPassword { get; set; } = String.Empty;
}
