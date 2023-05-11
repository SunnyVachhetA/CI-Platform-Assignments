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

    public async Task<int> DeleteAllNotificationAsync(long userId)
    {
        var query = "DELETE FROM user_notification WHERE user_id = {0}";
        return await _dbContext.Database.ExecuteSqlRawAsync(query, userId);
    }

    public async Task<IEnumerable<UserNotification>> FetchAllUserNotification(long id) => await dbSet
            .Include(notification => notification.Notification)
            .Where(notification => notification.UserId == id)
            .OrderByDescending(notification => notification.CreatedAt)
            .ToListAsync();

    public async Task<int> UpdateReadStatus(long userId, long notifsId)
    {
        var query = "UPDATE user_notification SET is_read = {0}, updated_at = {1} WHERE user_id = {2} AND notification_id = {3}";
        return await _dbContext.Database.ExecuteSqlRawAsync(query, true, DateTimeOffset.Now, userId, notifsId);
    }
}
