using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;

public interface IUserNotificationService
{
    Task<NotificationContainerVM> LoadAllNotificationsAsync(long id);
    Task MarkNotificationAsReadAsync(long userId, long notifsId);
    Task DeleteAllNotification(long userId);
}
