using CISkillMaster.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace CISkillMaster.Entities.DTO;

public class SkillFormDTO
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public bool? Status { get; set; }  
}
