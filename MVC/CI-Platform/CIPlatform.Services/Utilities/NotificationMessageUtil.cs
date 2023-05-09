
namespace CIPlatform.Services.Utilities;

public static class NotificationMessageUtil
{
    public static string NewsSubject { get; private set; }  = "CI Platform | New CMS Page";
    public static string NewMissionSubject { get; private set; }  = "CI Platform | New Mission";
    public static string MissionApplicationApprove { get; private set; } = "CI Platform | Mission Application Approval Notification";
    public static string MissionApplicationDecline { get; private set; } = "CI Platform | Mission Application Decline Notification";

    public static string VolunteerHourSubject { get; private set; } = "CI Platform | Volunteer Hour Entry Notification";

    public static string VolunteerGoalSubject { get; private set; } = "CI Platform | Volunteer Goal Entry Notification";

    public static string MyStoryApproval { get; private set; } = "CI Platform | Story Approval Notification";

    public static string MyStoryDecline { get; private set; } = "CI Platform | Story Decline Notification";

    public static string CommentApproval { get; private set; } = "CI Platform | Comment Approval Notification";
    public static string CommentDecline { get; private set; } = "CI Platform | Comment Decline Notification";

    public static string RecommendMissionSubject { get; private set; } = "CI Platform | Recommend Mission Notification";
    public static string RecommendStorySubject { get; private set; } = "CI Platform | Recommend Story Notification";

}
