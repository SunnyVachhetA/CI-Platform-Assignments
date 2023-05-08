using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Services.Utilities;

public static class NotificationMailMessageUtil
{
    public static string GetSubjectForMenu(NotificationTypeMenu menu, bool approvalStatus = false)
        => menu switch
        {
            NotificationTypeMenu.NEWS => NotificationMessageUtil.NewsSubject,
            NotificationTypeMenu.RECOMMEND_MISSION => throw new NotImplementedException(),
            NotificationTypeMenu.VOLUNTEER_HOURS => NotificationMessageUtil.VolunteerHourSubject,
            NotificationTypeMenu.VOLUNTEER_GOALS => NotificationMessageUtil.VolunteerGoalSubject,
            NotificationTypeMenu.MY_COMMENT => throw new NotImplementedException(),
            NotificationTypeMenu.MY_STORIES => throw new NotImplementedException(),
            NotificationTypeMenu.NEW_MISSIONS => NotificationMessageUtil.NewMissionSubject,
            NotificationTypeMenu.RECOMMEND_STORY => throw new NotImplementedException(),
            NotificationTypeMenu.MISSION_APPLICATION => approvalStatus ? NotificationMessageUtil.MissionApplicationApprove : NotificationMessageUtil.MissionApplicationDecline,
            NotificationTypeMenu.RECEIVE_BY_EMAIL => throw new NotImplementedException(),
            _ => throw new NotImplementedException()
        };
    public static string GetMessageForMenu(NotificationTypeMenu menu, string title, string pageLink, string userName, bool approvalStatus = false)
        => menu switch
        {
            NotificationTypeMenu.NEWS => MailMessageFormatUtility.GenerateMessageForNewCMSPage(title, pageLink, userName),
            NotificationTypeMenu.RECOMMEND_MISSION => throw new NotImplementedException(),
            NotificationTypeMenu.VOLUNTEER_HOURS => approvalStatus ?
                                                        MailMessageFormatUtility.GenerateMessageForVolunteerHourApprove(title, pageLink, userName)
                                                        : MailMessageFormatUtility.GenerateMessageForVolunteerHourDecline(title, pageLink, userName),

            NotificationTypeMenu.VOLUNTEER_GOALS => approvalStatus ?
                                                        MailMessageFormatUtility.GenerateMessageForVolunteerGoalApprove(title, pageLink, userName)
                                                        : MailMessageFormatUtility.GenerateMessageForVolunteerGoalDecline(title, pageLink, userName),

            NotificationTypeMenu.MY_COMMENT => throw new NotImplementedException(),

            NotificationTypeMenu.MY_STORIES => approvalStatus ?
                                                        MailMessageFormatUtility.GenerateMessageForStoryApproval(title, pageLink, userName)
                                                        : MailMessageFormatUtility.GenerateMessageForStoryDecline(title, pageLink, userName),

            NotificationTypeMenu.NEW_MISSIONS => MailMessageFormatUtility.GenerateMessageForNewMission(title, pageLink, userName),
            NotificationTypeMenu.NEW_MESSAGES => throw new NotImplementedException(),
            NotificationTypeMenu.RECOMMEND_STORY => throw new NotImplementedException(),

            NotificationTypeMenu.MISSION_APPLICATION => approvalStatus ?
                                                        MailMessageFormatUtility.GenerateMessageForMissionApplicationApprove(title, pageLink, userName)
                                                        : MailMessageFormatUtility.GenerateMessageForMissionApplicationDecline(title, pageLink, userName),

            NotificationTypeMenu.RECEIVE_BY_EMAIL => throw new NotImplementedException(),
            _ => throw new NotImplementedException()
        };
}
