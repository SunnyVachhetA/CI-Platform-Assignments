using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;

public interface IUserNotificationRepository : IRepository<UserNotification>
{
    Task<IEnumerable<UserNotification>> FetchAllUserNotification(long id);
    Task<int> UpdateReadStatus(long userId, long notifsId);
    Task<int> DeleteAllNotificationAsync(long userId);

    Task<List<NotificationSetting>> SaveNotificaitonUsingSPAsync(string message, NotificationTypeEnum notificationType, NotificationTypeMenu menu, string columnName);
}
