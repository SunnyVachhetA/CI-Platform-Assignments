
using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class ContactUsVM
{
    public long UserId { get; set; }
    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Please enter subject!")]
    [Display(Name="Subject*")]
    [RegularExpression("^[a-zA-Z0-9]{10, 120}$", ErrorMessage = "At least 10 and maximum 120 character allowed!")]
    public string Subject { get; set; }

    [Required(ErrorMessage = "Please enter message!")]
    [Display(Name="Message*")]
    [RegularExpression("^[a-zA-Z0-9]{15,}$", ErrorMessage = "At least 15 characters are required!")]
    public string Message { get; set; }
}
