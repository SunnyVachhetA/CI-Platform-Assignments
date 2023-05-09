using CIPlatform.Entities.VMConstants;
using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;

public class CommentAdminVM
{
    public long CommentId { get; set; }
    public long UserId { get; set; }
    [Display(Name = "User Name")]
    public string UserName { get; set; } = string.Empty;
    public long MissionId { get; set; }
    [Display(Name ="Mission Title")]
    public string MissionTitle { get; set; } = string.Empty;
    public ApprovalStatus ApprovalStatus { get; set; }
    public DateTimeOffset CommentDate { get; set; }

    [Display(Name ="Comment")]
    public string CommentText { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
