
namespace CIPlatform.Services.Utilities;

public static class NotificationMessageUtil
{
    public static string NewsSubject { get; private set; }  = "CI Platform | New CMS Page";
    public static string NewMissionSubject { get; private set; }  = "CI Platform | New Mission";
    public static string MissionApplicationApprove { get; private set; } = "CI Platform | Mission Application Approval Notification";
    public static string MissionApplicationDecline { get; private set; } = "CI Platform | Mission Application Decline Notification";

    public static string VolunteerHourSubject { get; private set; } = "CI Platform | Volunteer Hour Entry Notification";

    public static string VolunteerGoalSubject { get; private set; } = "CI Platform | Volunteer Goal Entry Notification";

}
