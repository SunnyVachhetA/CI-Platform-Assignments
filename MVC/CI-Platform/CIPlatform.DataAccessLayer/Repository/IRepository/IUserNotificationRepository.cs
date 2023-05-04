using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;

public interface IUserNotificationRepository : IRepository<UserNotification>
{
    Task<IEnumerable<UserNotification>> FetchAllUserNotification(long id);
}
