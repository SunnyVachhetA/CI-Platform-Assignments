using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.VMConstants;
public enum ContactStatus
{
    [Display(Name="Pending")]
    PENDING,

    [Display(Name="Replied")]
    REPLIED,

    [Display(Name = "Decline")]
    DECLINE
}
