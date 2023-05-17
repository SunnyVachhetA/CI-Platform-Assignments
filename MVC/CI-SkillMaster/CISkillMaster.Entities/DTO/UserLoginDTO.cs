using System.ComponentModel.DataAnnotations;
namespace CISkillMaster.Entities.DTO;
public class UserLoginDTO
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(255, ErrorMessage = "Email can have maximum 255 character.")]
    [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
+ "@"
+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Please enter valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [MaxLength(255, ErrorMessage = "Password can have maximum 255 character.")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password should have at least 8 character, 1 lower, 1 upper and special symbol.")]
    public string Password { get; set; } = string.Empty;
}
