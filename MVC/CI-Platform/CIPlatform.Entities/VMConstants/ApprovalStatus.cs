using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.VMConstants;
public enum ApprovalStatus
{
    [Display(Name="Pending")]
    PENDING,
    [Display(Name="Approved")]
    APPROVED,
    [Display(Name="Declined")]
    DECLINED
}
