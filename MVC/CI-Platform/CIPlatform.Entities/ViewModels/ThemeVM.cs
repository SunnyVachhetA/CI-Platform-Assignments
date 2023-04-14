using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class ThemeVM
{
    public short ThemeId { get; set; }

    [Required(ErrorMessage = "Please enter title!")]
    [Display(Name="Theme Title")]
    public string? Title { get; set; }

    [Required]
    public bool? Status { get; set; }

    public DateTimeOffset? LastModified { get; set; }
}
