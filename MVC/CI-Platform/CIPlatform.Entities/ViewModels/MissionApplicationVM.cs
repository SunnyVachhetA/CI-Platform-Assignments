using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Entities.ViewModels;

public class MissionApplicationVM
{
    public long ApplicationId { get; set; }
    public string MissionTitle { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public long UserId { get; set; }
    public DateTimeOffset AppliedAt { get; set; }
    public long MissionId { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; }

    public string Email { get; set; } = string.Empty;

}