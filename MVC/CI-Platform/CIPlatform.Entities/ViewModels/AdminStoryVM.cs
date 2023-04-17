using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Entities.ViewModels;
public class AdminStoryVM
{
    public long StoryId { get; set; }
    public string Title { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string MissionTitle { get; set; } = string.Empty;

    public UserStoryStatus StoryStatus { get; set; }

    public bool IsDeleted { get; set; }

    public bool  Status { get; set; }
}
