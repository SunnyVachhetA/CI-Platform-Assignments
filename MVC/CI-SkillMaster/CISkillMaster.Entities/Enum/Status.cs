using System.ComponentModel.DataAnnotations;

namespace CISkillMaster.Entities.Enum;

public enum Status
{
    Deleted,
    Active,
    [Display(Name = "In Active")]
    InActive
}
