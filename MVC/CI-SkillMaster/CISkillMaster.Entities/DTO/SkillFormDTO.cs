using System.ComponentModel.DataAnnotations;

namespace CISkillMaster.Entities.DTO;

public class SkillFormDTO
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [MinLength(3, ErrorMessage = "Skill name must have at least 3 characters.")]
    [MaxLength(30, ErrorMessage = "Skill name can have max 30 characters!")]
    [Display(Name = "Skill Name")]
    public string Title { get; set; } = string.Empty;

    [Required]
    public bool? Status { get; set; }
}
