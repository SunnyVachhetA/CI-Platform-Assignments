using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;

public class UserNotificationCheckRepository : Repository<UserNotificationCheck>, IUserNotificationCheckRepository
{
    public UserNotificationCheckRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
}
