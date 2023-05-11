using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;

public class UserNotificationService : IUserNotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    public UserNotificationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<NotificationContainerVM> LoadAllNotificationsAsync(long id)
    {
        NotificationContainerVM vm = new();

        var notifications = await _unitOfWork.UserNotificationRepo.FetchAllUserNotification(id);
        var userLastCheck = await _unitOfWork.UserNotificationCheckRepo.GetFirstOrDefaultAsync(check => check.UserId == id);

        var lastCheckDate = userLastCheck?.LastCheck ?? DateTimeOffset.MinValue;
        vm.NewNotifications = notifications
                        .Where(notification => lastCheckDate < notification.CreatedAt)
                        .Select(ConvertToUserNotificationVM);

        vm.OldNotifications = notifications
                       .Where(notification => lastCheckDate > notification.CreatedAt)
                       .Select(ConvertToUserNotificationVM);

        vm.UnreadCount = notifications.Count(notification => !notification.IsRead);

        vm.NotificationSetting = NotificationSettingService.ConvertModelToSettingVM(await _unitOfWork.NotificationSettingRepo.GetFirstOrDefaultAsync(setting => setting.UserId == id)!);
        return vm;
    }

    private static UserNotificationVM ConvertToUserNotificationVM(UserNotification notification)
    {
        UserNotificationVM vm = new()
        {
            Id = notification.NotificationId,
            UserId = notification.UserId,
            Message = notification.Notification.Message,
            IsRead = notification.IsRead,
            NotificationType = (NotificationTypeEnum) notification.Notification.NotificationType,
            FromUserAvatar = notification.Notification.FromUserAvatar,
            CreatedAt = notification.CreatedAt,
        };
        return vm;
    }

    public async Task MarkNotificationAsReadAsync(long userId, long notifsId)
    {
        int result = await _unitOfWork.UserNotificationRepo.UpdateReadStatus(userId, notifsId);
        if (result == 0) throw new Exception("Something went during mark as read: " + userId);
    }

    public async Task DeleteAllNotification(long userId)
    {
        int result = await _unitOfWork.UserNotificationRepo.DeleteAllNotificationAsync(userId);
        if(result == 0) throw new Exception("Something went wrong during delete all notification: " + nameof(userId));
    }
}
