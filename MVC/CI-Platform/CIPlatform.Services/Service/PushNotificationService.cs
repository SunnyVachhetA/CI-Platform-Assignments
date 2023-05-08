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

        foreach(var user in userList)
        {
            if (user.IsOpenForEmail) emailSubsciptionList.Add( new UserContactVM() { UserId = user.UserId, UserName = user.UserName, Email = user.Email! } );

            UserNotification userNotification = new()
            {
                Notification = new()
                {
                    Message = message,
                    NotificationType = (byte)notificationType,
                    NotificationFor = (byte)menu
                },
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
        UserNotification userNotification = new()
        {
            Notification = new()
            {
                Message = template.Message,
                NotificationType = (byte)template.Type,
                NotificationFor = (byte)template.NotificationFor
            },
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

            case NotificationTypeMenu.MY_COMMENT:
                vm = await _unitOfWork.NotificationSettingRepo.GetUserSettingAsync(setting => setting.UserId == template.UserId && (setting.IsEnabledMessage?? false));
                isOpenForNotification= vm?.IsEnabledMessage ?? false;
                break;
        }
        NotificationSettingVM result = null!;
        if(vm is not null)
            result = NotificationSettingService.ConvertModelToSettingVM(vm);
        return (result, isOpenForNotification);
    }
}
