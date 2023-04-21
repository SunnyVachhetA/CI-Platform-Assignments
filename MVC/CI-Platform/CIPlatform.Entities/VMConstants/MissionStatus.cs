using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.VMConstants;
public enum MissionStatus
{
    [Display(Name="ONGOING")]
    ONGOING,
    [Display(Name="Finished")]
    FINISHED
}
