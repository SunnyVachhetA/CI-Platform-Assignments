using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

    public async Task<List<NotificationSetting>> SaveNotificaitonUsingSPAsync(string message, NotificationTypeEnum notificationType, NotificationTypeMenu menu, string columnName)
    {

        var result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC usp_Notification_PushNotificationToAllUsers @Message, @NotificationType, @NotificationFor, @Column, @CreatedAt",
        new SqlParameter("@Message", message),
                    new SqlParameter("@NotificationType", (int)notificationType),
                    new SqlParameter("@NotificationFor", (int)menu),
                    new SqlParameter("@Column", columnName),
                    new SqlParameter("@CreatedAt", DateTimeOffset.Now));

        string query = $"SELECT * FROM notification_setting WHERE {columnName} = 1";

        var usersOpenForEmail = await _dbContext.NotificationSettings.FromSqlRaw(query)
            .Include(user => user.User)
            .Where(setting => setting.User.Status ?? false)
            .ToListAsync();

        return usersOpenForEmail;
    }

    public async Task<bool> SaveUserNotificationUsingSPAsync(UserNotificationTemplate template, string columnName)
    {
        var result = await _dbContext.Database.ExecuteSqlRawAsync(
           "EXEC usp_UserNotification_SaveUserNotification " +
           "@Message, @NotificationType, @NotificationFor, @UserId, @Column, @CreatedAt",
           new SqlParameter("@Message", template.Message),
           new SqlParameter("@NotificationType", (int)template.Type),
           new SqlParameter("@NotificationFor", (int)template.NotificationFor),
           new SqlParameter("@UserId", template.UserId),
           new SqlParameter("@Column", columnName),
           new SqlParameter("@CreatedAt", DateTimeOffset.Now));


        string query = $"SELECT * FROM notification_setting WHERE {columnName} = 1 AND is_enalbed_email = 1 AND user_id = {template.UserId}";
        NotificationSetting setting = _dbContext.NotificationSettings.FromSqlRaw(query)?.FirstOrDefault()!;

        return setting is not null;
    }


}

/*
 
        string query = $"SELECT * FROM notification_settings WHERE {columnName} = 1";

        var usersOpenForEmail = await _dbContext.NotificationSettings.FromSqlRaw(query)
            .Include(user => user)
            .Where(setting => setting.User.Status?? false)
            .ToListAsync();
 */
