using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.VMConstants;
public enum UserStoryStatus
{
    DRAFT,
    [Display(Name="Pending")]
    PENDING,
    [Display(Name="Approved")]
    APPROVED,
    [Display(Name="Declined")]
    DECLINED
}
