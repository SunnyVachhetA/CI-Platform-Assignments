using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;

public interface IUserNotificationService
{
    Task<NotificationContainerVM> LoadAllNotificationsAsync(long id);
}
