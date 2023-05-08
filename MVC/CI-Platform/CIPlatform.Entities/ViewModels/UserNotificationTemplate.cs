using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Entities.ViewModels;

public class UserNotificationTemplate
{
    public string Title { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public long UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public NotificationTypeEnum Type { get; set; }
    public NotificationTypeMenu NotificationFor { get; set; }
    public string Message { get; set; } = string.Empty;

    public static UserNotificationTemplate ConvertFromMissionApplication(MissionApplicationVM application)
    {
        UserNotificationTemplate template = new()
        {
            UserId = application.UserId,
            Message = application.ApprovalStatus == ApprovalStatus.APPROVED ? 
                      $"Volunteering request has been approved for this mission: {application.MissionTitle}" 
                      : $"Volunteering request has been declined for this mission: {application.MissionTitle}",
            Type = application.ApprovalStatus == ApprovalStatus.APPROVED ? NotificationTypeEnum.APPROVE : NotificationTypeEnum.DECLINE,
            NotificationFor = NotificationTypeMenu.MISSION_APPLICATION,
            Email = application.Email,
            UserName = application.UserName,
            Title = application.MissionTitle
        };
        return template;
    }

    public static UserNotificationTemplate ConvertFromTimesheet(VolunteerTimesheetVM timesheet)
    {
        UserNotificationTemplate template = new()
        {
           UserId = timesheet.UserId,
           Message = GetTimesheetMessage(timesheet),
           Type = timesheet.Status == ApprovalStatus.APPROVED ? NotificationTypeEnum.APPROVE : NotificationTypeEnum.DECLINE,
           NotificationFor= timesheet.MissionType == MissionTypeEnum.TIME ? NotificationTypeMenu.VOLUNTEER_HOURS : NotificationTypeMenu.VOLUNTEER_GOALS,
           Email = timesheet.Email,
           UserName= timesheet.UserName,
           Title = timesheet.MissionTitle
        };
        return template;
    }

    public static UserNotificationTemplate ConvertFromContact(ContactUsVM contact)
    {
        UserNotificationTemplate template = new()
        {
            UserId = contact.UserId,    
            UserName = contact.UserName,
            Message = "Admin has sent you contact response on email.",
            NotificationFor = NotificationTypeMenu.MY_COMMENT,
            Type = NotificationTypeEnum.NEW,
            Email = contact.Email
        };
        return template;
    }
    #region Helper Methods
    private static string GetTimesheetMessage(VolunteerTimesheetVM timesheet)
    {
        string message = string.Empty;
        if(timesheet.Status == ApprovalStatus.APPROVED)
        {
            message = timesheet.MissionType == MissionTypeEnum.TIME ?
                  $"Volunteer hour entry apprvoed for this mission: {timesheet.MissionTitle}"
                : $"Volunteer hour entry declined for this mission: {timesheet.MissionTitle}";
        }
        else
        {
            message = timesheet.MissionType == MissionTypeEnum.GOAL ?
                  $"Volunteer goal entry apprvoed for this mission: {timesheet.MissionTitle}"
                : $"Volunteer goal entry declined for this mission: {timesheet.MissionTitle}";
        }
        return message;
    }
    #endregion 
}
