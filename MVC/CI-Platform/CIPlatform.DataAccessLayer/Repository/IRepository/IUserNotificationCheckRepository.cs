using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;

public interface IUserNotificationCheckRepository : IRepository<UserNotificationCheck>
{
    Task<int> UpdateUserLastCheckAsync(long userId);
}
