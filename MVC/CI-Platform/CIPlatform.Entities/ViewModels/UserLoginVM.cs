using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class UserLoginVM
{

    [Required]
    [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
+ "@"
+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Invalid Email ID.")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must have at least 8 character.")]
    public string Password { get; set; } = string.Empty;

    public IEnumerable<BannerVM>? Banners { get; set; } = new List<BannerVM>();

    public string? ReturnUrl { get; set; }
}
