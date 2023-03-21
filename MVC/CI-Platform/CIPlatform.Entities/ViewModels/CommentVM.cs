using CIPlatform.Entities.VMConstants;
namespace CIPlatform.Entities.ViewModels;
public class CommentVM
{ 
    public long? CommentId { get; set; }
    public long UserId { get; set; }
    public long MissionId { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.PENDING;
    public string Avatar { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get;set; }
    public bool CommentExists { get; set; } = false;
    public string UserName { get; set; } = string.Empty;
}
