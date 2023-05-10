using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Services.Utilities;

public static class NotificationMailMessageUtil
{
    public static string GetSubjectForMenu(NotificationTypeMenu menu, bool approvalStatus = false)
        => menu switch
        {
            NotificationTypeMenu.NEWS => NotificationMessageUtil.NewsSubject,
            NotificationTypeMenu.RECOMMEND_MISSION => NotificationMessageUtil.RecommendMissionSubject,
            NotificationTypeMenu.RECOMMEND_STORY => NotificationMessageUtil.RecommendStorySubject,

            NotificationTypeMenu.VOLUNTEER_HOURS => NotificationMessageUtil.VolunteerHourSubject,
            NotificationTypeMenu.VOLUNTEER_GOALS => NotificationMessageUtil.VolunteerGoalSubject,

            NotificationTypeMenu.MY_COMMENT => approvalStatus ? NotificationMessageUtil.CommentApproval : NotificationMessageUtil.CommentDecline,

            NotificationTypeMenu.MY_STORIES => approvalStatus ? NotificationMessageUtil.MyStoryApproval : NotificationMessageUtil.MyStoryDecline,

            NotificationTypeMenu.NEW_MISSIONS => NotificationMessageUtil.NewMissionSubject,

            NotificationTypeMenu.MISSION_APPLICATION => approvalStatus ? NotificationMessageUtil.MissionApplicationApprove : NotificationMessageUtil.MissionApplicationDecline,

            _ => throw new NotImplementedException()
        };
    public static string GetMessageForMenu(NotificationTypeMenu menu, string title, string pageLink, string userName, bool approvalStatus = false)
        => menu switch
        {
            NotificationTypeMenu.NEWS => MailMessageFormatUtility.GenerateMessageForNewCMSPage(title, pageLink, userName),

            NotificationTypeMenu.VOLUNTEER_HOURS => approvalStatus ?
                                                        MailMessageFormatUtility.GenerateMessageForVolunteerHourApprove(title, pageLink, userName)
                                                        : MailMessageFormatUtility.GenerateMessageForVolunteerHourDecline(title, pageLink, userName),

            NotificationTypeMenu.VOLUNTEER_GOALS => approvalStatus ?
                                                        MailMessageFormatUtility.GenerateMessageForVolunteerGoalApprove(title, pageLink, userName)
                                                        : MailMessageFormatUtility.GenerateMessageForVolunteerGoalDecline(title, pageLink, userName),

            NotificationTypeMenu.MY_COMMENT => approvalStatus ?
                                                        MailMessageFormatUtility.GenerateMessageForCommentApproval(title, pageLink, userName)
                                                        : MailMessageFormatUtility.GenerateMessageForCommentDecline(title, pageLink, userName),

            NotificationTypeMenu.MY_STORIES => approvalStatus ?
                                                        MailMessageFormatUtility.GenerateMessageForStoryApproval(title, pageLink, userName)
                                                        : MailMessageFormatUtility.GenerateMessageForStoryDecline(title, pageLink, userName),

            NotificationTypeMenu.NEW_MISSIONS => MailMessageFormatUtility.GenerateMessageForNewMission(title, pageLink, userName),


            NotificationTypeMenu.MISSION_APPLICATION => approvalStatus ?
                                                        MailMessageFormatUtility.GenerateMessageForMissionApplicationApprove(title, pageLink, userName)
                                                        : MailMessageFormatUtility.GenerateMessageForMissionApplicationDecline(title, pageLink, userName),


            _ => throw new NotImplementedException()
        };

}
