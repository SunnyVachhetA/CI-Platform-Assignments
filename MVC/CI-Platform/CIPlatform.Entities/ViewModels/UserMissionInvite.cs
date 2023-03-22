namespace CIPlatform.Entities.ViewModels;
public class UserMissionInviteVM
{
    public long FromUserId { get; set; }
    public long ToUserId { get; set; }
    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public bool IsInvited { get; set; } = false;

    public string Avatar { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public long MissionId { get; set; }
}
