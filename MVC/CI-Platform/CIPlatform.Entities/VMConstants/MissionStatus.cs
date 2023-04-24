using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.VMConstants;
public enum MissionStatus
{
    [Display(Name="Ongoing")]
    ONGOING,
    [Display(Name="Finished")]
    FINISHED
}
