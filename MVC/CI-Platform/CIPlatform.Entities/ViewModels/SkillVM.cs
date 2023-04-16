using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class SkillVM
{
    public short SkillId { get; set; }

    [Required(ErrorMessage = "Please enter title!")]
    [Display(Name = "Skill Title")]
    public string Name { get; set; } = null!;

    [Required]
    public bool? Status { get; set; }

    public DateTimeOffset? LastModified;
}
