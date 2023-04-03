using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class ChangePasswordVM
{ 
    public long UserId { get; set; }

    [Required(ErrorMessage = "Enter Old Password")]
    [Display(Name = "Old Password")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "Enter New Password")]
    [Display(Name = "New Password")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password have at least 8 character, 1 lower, 1 upper and special symbol!")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Enter Confirm Password")]
    [Display(Name = "Confirm Password")]
    [Compare("NewPassword", ErrorMessage = "Confirm Password must be same as New Password!")]
    public string ConfirmPassword { get; set; }
}
