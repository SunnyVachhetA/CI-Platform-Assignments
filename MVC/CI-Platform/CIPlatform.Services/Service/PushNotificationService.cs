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

    private readonly IEmailService _emailService;
    public PushNotificationService(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task PushEmailNotificationToSubscriberAsync(string title, string pageLink, List<UserContactVM> emailSubscriptionList, NotificationTypeMenu menu)
    {
        string subject = NotificationMailMessageUtil.GetSubjectForMenu(menu);
        foreach (var userContact in emailSubscriptionList)
        {
            string userName = userContact.UserName;
            string email = userContact.Email;
            string message = NotificationMailMessageUtil.GetMessageForMenu(menu, title, pageLink, userName);
            _ = _emailService.EmailSendAsync(email, subject, message);
        }
    }

    public async Task PushEmailNotificationToUserAsync(UserNotificationTemplate template, string link)
    {
        bool notificationType = template.Type == NotificationTypeEnum.APPROVE;
        string subject = NotificationMailMessageUtil.GetSubjectForMenu(template.NotificationFor, notificationType);
        string message = NotificationMailMessageUtil.GetMessageForMenu(template.NotificationFor, template.Title, link, template.UserName, notificationType);

        _ = _emailService.EmailSendAsync(template.Email, subject, message);
    }

    public async Task<List<UserContactVM>> PushNotificationToAllUsers(string message, NotificationTypeEnum notificationType, NotificationTypeMenu menu)
    {
        var userList = await GetAllUserPreference(menu);
        
        List<UserContactVM> emailSubsciptionList = new();
        Notification notification = new()
        {
            Message = message,
            NotificationType = (byte)notificationType,
            NotificationFor = (byte)menu
        };
        await _unitOfWork.NotificationRepo.AddAsync(notification);
        await _unitOfWork.SaveAsync();
        foreach (var user in userList)
        {
            if (user.IsOpenForEmail) emailSubsciptionList.Add( new UserContactVM() { UserId = user.UserId, UserName = user.UserName, Email = user.Email! } );

            UserNotification userNotification = new()
            {
                NotificationId = notification.NotificationId,
                CreatedAt = DateTime.Now,
                UserId = user.UserId,
                IsRead = false,
            };  

            await _unitOfWork.UserNotificationRepo.AddAsync(userNotification);
        }

        await _unitOfWork.SaveAsync();
        return emailSubsciptionList;
    }

    public async Task<bool> PushNotificationToUserAsync(UserNotificationTemplate template)
    {
        var userPreference = await GetUserPreference(template);
        if (userPreference.Item1 is null || !userPreference.Item2) return false;
        Notification notification = new()
        {
            Message = template.Message,
            NotificationType = (byte)template.Type,
            NotificationFor = (byte)template.NotificationFor
        };
        await _unitOfWork.NotificationRepo.AddAsync(notification);
        await _unitOfWork.SaveAsync();
        UserNotification userNotification = new()
        {
            NotificationId = notification.NotificationId,
            CreatedAt = DateTime.Now,
            UserId = template.UserId,
            IsRead = false,
        };
        await _unitOfWork.UserNotificationRepo.AddAsync(userNotification);
        await _unitOfWork.SaveAsync();
        return userPreference.Item1.ReceiveByEmail;
    }

    private async Task<IEnumerable<UserNotificationSettingPreferenceVM>> GetAllUserPreference(NotificationTypeMenu menu)
    {
        IEnumerable<UserNotificationSettingPreferenceVM> userList = new List<UserNotificationSettingPreferenceVM>();
        userList = menu switch
        {
            NotificationTypeMenu.NEWS => await _unitOfWork.NotificationSettingRepo.GetUserListAsync(setting => setting.IsEnabledNews),
            NotificationTypeMenu.NEW_MISSIONS => await _unitOfWork.NotificationSettingRepo.GetUserListAsync(setting => setting.IsEnabledNewMission),
            _ => throw new KeyNotFoundException($"Notification type not found: {nameof(menu)}"),
        };
        return userList;
    }

    private async Task<(NotificationSettingVM?,  bool)> GetUserPreference(UserNotificationTemplate template)
    {
        NotificationSetting? vm = null;
        bool isOpenForNotification = false;
        switch(template.NotificationFor)
        {
            case NotificationTypeMenu.MISSION_APPLICATION:
                vm = await _unitOfWork.NotificationSettingRepo.GetUserSettingAsync( setting => setting.UserId == template.UserId && (setting.IsEnabledMissionApplication?? false));
                isOpenForNotification = vm?.IsEnabledMissionApplication?? false;
                break;

            case NotificationTypeMenu.VOLUNTEER_HOURS:
                vm = await _unitOfWork.NotificationSettingRepo.GetUserSettingAsync(setting => setting.UserId == template.UserId && (setting.IsEnabledVolunteerHour ?? false));
                isOpenForNotification = vm?.IsEnabledVolunteerHour ?? false;
                break;

            case NotificationTypeMenu.VOLUNTEER_GOALS:
                vm = await _unitOfWork.NotificationSettingRepo.GetUserSettingAsync(setting => setting.UserId == template.UserId && (setting.IsEnabledVolunteerGoal ?? false));
                isOpenForNotification = vm?.IsEnabledVolunteerGoal ?? false;
                break;

            case NotificationTypeMenu.NEW_MESSAGES:
                vm = await _unitOfWork.NotificationSettingRepo.GetUserSettingAsync(setting => setting.UserId == template.UserId && (setting.IsEnabledMessage?? false));
                isOpenForNotification= vm?.IsEnabledMessage ?? false;
                break;

            case NotificationTypeMenu.MY_STORIES:
                vm = await _unitOfWork.NotificationSettingRepo.GetUserSettingAsync(setting => setting.UserId == template.UserId && (setting.IsEnabledStory ?? false));
                isOpenForNotification = vm?.IsEnabledStory ?? false;
                break;

            case NotificationTypeMenu.MY_COMMENT:
                vm = await _unitOfWork.NotificationSettingRepo.GetUserSettingAsync(setting => setting.UserId == template.UserId && (setting.IsEnabledComment));
                isOpenForNotification = vm?.IsEnabledComment ?? false;
                break;
        }
        NotificationSettingVM result = null!;
        if(vm is not null)
            result = NotificationSettingService.ConvertModelToSettingVM(vm);
        return (result, isOpenForNotification);
    }
    
    public async Task PushRecommendNotificationToUsersAsync(string message, IEnumerable<UserNotificationSettingPreferenceVM> userPrefrence, string avatar, NotificationTypeEnum type, NotificationTypeMenu menu)
    {
        Notification notification = new()
        {
            Message = message,
            NotificationType = (byte)type,
            NotificationFor = (byte)menu,
            FromUserAvatar = avatar,
        };
        await _unitOfWork.NotificationRepo.AddAsync(notification);
        await _unitOfWork.SaveAsync();
        foreach(var user in userPrefrence)
        {
            UserNotification userNotification = new()
            {
                NotificationId = notification.NotificationId,
                CreatedAt = DateTime.Now,
                UserId = user.UserId,
                IsRead = false,
            };

            await _unitOfWork.UserNotificationRepo.AddAsync(userNotification);
        }
        await _unitOfWork.SaveAsync();
    }

    public async Task PushRecommendEmailNotificationToUsersAsync(IEnumerable<UserNotificationSettingPreferenceVM> userPrefrence, string title, string link, string senderUserName, NotificationTypeMenu menu)
    {
        string subject = NotificationMailMessageUtil.GetSubjectForMenu(menu);
        foreach(var userContact in userPrefrence)
        {
            if (!userContact.IsOpenForEmail) continue;

            string email = userContact.Email;
            string userName = userContact.UserName;
            string message = (menu == NotificationTypeMenu.RECOMMEND_MISSION) ?
                MailMessageFormatUtility.GenerateMissionInviteMessage(senderUserName, link, userName, title)
                :MailMessageFormatUtility.GenerateStoryInviteMessage(senderUserName, link, userName, title);
            _ = _emailService.EmailSendAsync(email, subject, message);
        }
    }


    #region Push notification using user stored procedure
    public async Task<List<UserContactVM>> PushNotificationToAllUsersSPAsync(string message, NotificationTypeEnum notificationType, NotificationTypeMenu menu)
    {
        string columnName = GetColumnNameFromMenu(menu);
        var result = await _unitOfWork.UserNotificationRepo.SaveNotificaitonUsingSPAsync(message, notificationType, menu, columnName);

        return result.Select( userPref => new UserContactVM()
        {
            UserName = userPref.User.FirstName + " " + userPref.User.LastName,
            UserId = userPref.UserId,
            Email = userPref.User.Email,
        }).ToList();
    }

    private static string GetColumnNameFromMenu(NotificationTypeMenu menu) =>
        menu switch
        {
            NotificationTypeMenu.NEWS => "is_enabled_news",

            _ => throw new NotImplementedException()
        };
    #endregion
}
