using CIPlatform.Entities.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class UserRegistrationVM
{
    public long? UserId { get; set; }
    
    [Required]
    [DisplayName("First Name")]
    [MinLength(3, ErrorMessage = "First name should have at least 3 characters.")]
    [MaxLength(16, ErrorMessage = "First name can have max 16 characters.")]
    [RegularExpression(@"^\S+$", ErrorMessage = "First name must not contain only whitespace characters.")]
    public string FirstName { get; set; } = null!;

    [Required]
    [DisplayName("Last Name")]
    [MinLength(3, ErrorMessage = "Last name should have at least 3 characters.")]
    [MaxLength(16, ErrorMessage = "Last name can have max 16 characters.")]
    [RegularExpression(@"^\S+$", ErrorMessage = "Last name must not contain only whitespace characters.")]
    public string LastName { get; set; } = null!;

    [Required]
    [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
+ "@"
+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Invalid Email ID.")]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must have at least 8 character.")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password have at least 8 character, 1 lower, 1 upper and special symbol.")]

    public string Password { get; set; } = null!;

    [Required]
    [DisplayName("Phone Number")]
    [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone number should be of 10 digit.")]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [Compare("Password", ErrorMessage = "Confirm password must be same as password.")]
    [DisplayName("Confirm Password")]
    public string ConfirmPassword { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    public string Avatar { get; set; } = @"\images\static\anon-profile.png";

    public bool Status { get; set; } = false;

    public string? EmployeeID { get; set; }
    public string? Department { get; set; }

    public IEnumerable<BannerVM>? Banners { get; set; } = new List<BannerVM>();

    public long? AdminId { get; set; }
}
