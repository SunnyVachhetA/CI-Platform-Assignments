using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.VMConstants;
public enum MissionAvailability : byte
{
    [Display(Name="Daily")]
    DAILY = 1,

    [Display(Name="Weekend")]
    WEEK_END = 2,
    
    [Display(Name="Weekly")]
    WEEKLY = 3,
    
    [Display(Name="Monthly")]
    MONTHLY = 4
}