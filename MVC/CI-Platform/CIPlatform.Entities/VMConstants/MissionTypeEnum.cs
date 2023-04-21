using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.VMConstants;
public enum MissionTypeEnum
{
    [Display(Name="Goal")]
    GOAL,
    [Display(Name="Time")]
    TIME   
}
