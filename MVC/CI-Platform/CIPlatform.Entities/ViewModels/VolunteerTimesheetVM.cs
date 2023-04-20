using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Entities.ViewModels;
public class VolunteerTimesheetVM
{
    public long TimesheetId { get; set; }
    public long UserId { get; set; }
    public long MissionId { get; set; }
    public ApprovalStatus Status { get; set; }
    public string MissionTitle { get; set; } = string.Empty;
    public DateTimeOffset Date { get; set; }
    public int? Action { get; set; }
    public TimeSpan? Time { get; set; }
    public MissionTypeEnum MissionType { get; set; }

    public string Message = string.Empty;
    public string UserName = string.Empty;
}
