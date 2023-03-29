using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Entities.ViewModels;
public class ShareStoryVM
{
    public long StoryId { get; set; }
    public long UserId { get; set; }
    public long MissionId { get; set; }
    public string ThemeName { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string StoryTitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string UserAvatar { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string StoryThumbnail { get; set; } = string.Empty;
    public UserStoryStatus StoryStatus { get; set; } = UserStoryStatus.DRAFT;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
}
