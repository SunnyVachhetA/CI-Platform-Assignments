using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CIPlatform.DataAccessLayer.Repository;

public class NotificationSettingRepository : Repository<NotificationSetting>, INotificationSettingRepository
{
    private readonly CIDbContext _dbContext;
    public NotificationSettingRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserNotificationSettingPreferenceVM>> GetUserListAsync(Expression<Func<NotificationSetting, bool>> filter) 
    {
        string query = $"SELECT * FROM notification_setting";

        var result = await _dbContext.NotificationSettings
                    .FromSqlRaw(query)
                    .AsNoTracking()
                    .Include(notification => notification.User)
                    .Where(filter)
                    .Where(notification => notification.User.Status?? false)
                    .Select(notification => new UserNotificationSettingPreferenceVM()
                    {
                        UserId = notification.UserId,
                        UserName = $"{notification.User.FirstName} {notification.User.LastName}",
                        Email = notification.User.Email,
                        IsOpenForEmail = notification.IsEnabledEmail?? false
                    })
                    .ToListAsync();

        return result;
    }

    public async Task<NotificationSetting?> GetUserSettingAsync(Expression<Func<NotificationSetting, bool>> filter) =>
        await 
            dbSet
                .FirstOrDefaultAsync(filter);

}
