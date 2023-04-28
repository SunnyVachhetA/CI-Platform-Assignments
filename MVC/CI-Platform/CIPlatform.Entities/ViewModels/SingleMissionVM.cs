using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Entities.ViewModels;
public class SingleMissionVM
{
    public long MissionId { get; set; }
    public string Title { get; set; } = string.Empty;

    public MissionTypeEnum MissionType { get; set; }

    public DateTimeOffset? StartDate { get; set; }

    public DateTimeOffset? EndDate { get; set; }
}
