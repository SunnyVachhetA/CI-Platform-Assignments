using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;

public class UserNotificationRepository : Repository<UserNotification>, IUserNotificationRepository
{
    private readonly CIDbContext _dbContext;
    public UserNotificationRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext; 
    }

    public async Task<IEnumerable<UserNotification>> FetchAllUserNotification(long id) => await dbSet
            .Include(notification => notification.Notification)
            .Where(notification => notification.UserId == id)
            .ToListAsync();
}
