using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class ResetPasswordPostVM: PasswordResetVM
{
    [Required(ErrorMessage = "Password cannot be empty!")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password have at least 8 character, 1 lower, 1 upper and special symbol!")]

    public string Password { get; set; } = string.Empty;


    [Required(ErrorMessage = "Confirm password must be same as password!")]
    [Compare("Password")]
    [DisplayName("Confirm Password")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public IEnumerable<BannerVM>? Banners { get; set; } = new List<BannerVM>();
}
