using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;

public class UserNotificationCheckRepository : Repository<UserNotificationCheck>, IUserNotificationCheckRepository
{
    private readonly CIDbContext _dbContext;
    public UserNotificationCheckRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> UpdateUserLastCheckAsync(long userId)
    {
        var query = "UPDATE user_notification_check SET last_check = {0} WHERE user_id = {1}";
        return await _dbContext.Database.ExecuteSqlRawAsync(query, DateTimeOffset.Now, userId);
    }
}
