using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;

namespace CIPlatform.Services.Service;

public class PushNotificationService : IPushNotificationService
{
    private readonly IUnitOfWork _unitOfWork;

    public PushNotificationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task PushEmailNotificationToSubscriberAsync(string title, string pageLink, List<UserContactVM> emailSubscriptionList, NotificationTypeMenu menu)
    {
        string subject = GetSubjectForMenu(menu);

    }

    public async Task<List<UserContactVM>> PushNotificationToAllUsers(string message, NotificationTypeEnum notificationType, NotificationTypeMenu menu)
    {
        var userList = await GetAllUserPreference(menu);
        
        List<UserContactVM> emailSubsciptionList = new();

        foreach(var user in userList)
        {
            if (user.IsOpenForEmail) emailSubsciptionList.Add( new UserContactVM() { UserId = user.UserId, UserName = user.UserName, Email = user.Email! } );

            Notification notification_ = new()
            {
                Message = message,
                NotificationType = (byte)notificationType,
            };
            _unitOfWork.NotificationRepo.Add(notification_);
            UserNotification userNotification = new()
            {
                NotificationId = notification_.NotificationId,
                CreatedAt = DateTime.Now,
                UserId = user.UserId,
                IsRead = false,
            };  

            _unitOfWork.UserNotificationRepo.Add(userNotification);
        }

        _unitOfWork.Save();
        return emailSubsciptionList;
    }


    private async Task<IEnumerable<UserNotificationSettingPreferenceVM>> GetAllUserPreference(NotificationTypeMenu menu)
    {
        IEnumerable<UserNotificationSettingPreferenceVM> userList = new List<UserNotificationSettingPreferenceVM>();
        switch (menu)
        {
            case NotificationTypeMenu.NEWS:
                userList = await _unitOfWork.NotificationSettingRepo.GetUserListAsync("is_enabled_news", setting => setting.IsEnabledNews);
                break;
            case NotificationTypeMenu.NEW_MISSIONS:
                userList = await _unitOfWork.NotificationSettingRepo.GetUserListAsync("is_enabled_new_mission", setting => setting.IsEnabledNewMission);

                break;
            case NotificationTypeMenu.RECOMMEND_MISSION:
                userList = await _unitOfWork.NotificationSettingRepo.GetUserListAsync("is_enabled_recommend_mission", setting => setting.IsEnabledRecommendMission?? false);

                break;
            case NotificationTypeMenu.VOLUNTEER_HOURS:
                userList = await _unitOfWork.NotificationSettingRepo.GetUserListAsync("is_enabled_volunteer_hour", setting => setting.IsEnabledNews);

                break;
            case NotificationTypeMenu.VOLUNTEER_GOALS:
                userList = await _unitOfWork.NotificationSettingRepo.GetUserListAsync("is_enabled_comment", setting => setting.IsEnabledNews);

                break;
            case NotificationTypeMenu.MY_COMMENT:
                userList = await _unitOfWork.NotificationSettingRepo.GetUserListAsync("is_enabled_news", setting => setting.IsEnabledNews);

                break;
            case NotificationTypeMenu.MY_STORIES:
                userList = await _unitOfWork.NotificationSettingRepo.GetUserListAsync("is_enabled_story", setting => setting.IsEnabledNews);

                break;
            case NotificationTypeMenu.NEW_MESSAGES:
                userList = await _unitOfWork.NotificationSettingRepo.GetUserListAsync("is_enabled_message", setting => setting.IsEnabledNews);

                break;
            case NotificationTypeMenu.RECOMMEND_STORY:
                userList = await _unitOfWork.NotificationSettingRepo.GetUserListAsync("is_enabled_story", setting => setting.IsEnabledNews);

                break;
            case NotificationTypeMenu.MISSION_APPLICATION:
                userList = await _unitOfWork.NotificationSettingRepo.GetUserListAsync("is_enabled_mission_application", setting => setting.IsEnabledNews);

                break;
            case NotificationTypeMenu.RECEIVE_BY_EMAIL:
                userList = await _unitOfWork.NotificationSettingRepo.GetUserListAsync("is_enabled_email", setting => setting.IsEnabledNews);

                break;
            default:
                throw new KeyNotFoundException($"Notification type not found: {nameof(menu)}");
        }

        return userList;

    }

    private string GetSubjectForMenu(NotificationTypeMenu menu)
        => menu switch
        {
            NotificationTypeMenu.NEWS => NotificationMessageUtil.NewsSubject,
            NotificationTypeMenu.RECOMMEND_MISSION => throw new NotImplementedException(),
            NotificationTypeMenu.VOLUNTEER_HOURS => throw new NotImplementedException(),
            NotificationTypeMenu.VOLUNTEER_GOALS => throw new NotImplementedException(),
            NotificationTypeMenu.MY_COMMENT => throw new NotImplementedException(),
            NotificationTypeMenu.MY_STORIES => throw new NotImplementedException(),
            NotificationTypeMenu.NEW_MISSIONS => throw new NotImplementedException(),
            NotificationTypeMenu.NEW_MESSAGES => throw new NotImplementedException(),
            NotificationTypeMenu.RECOMMEND_STORY => throw new NotImplementedException(),
            NotificationTypeMenu.MISSION_APPLICATION => throw new NotImplementedException(),
            NotificationTypeMenu.RECEIVE_BY_EMAIL => throw new NotImplementedException()
        };
}
