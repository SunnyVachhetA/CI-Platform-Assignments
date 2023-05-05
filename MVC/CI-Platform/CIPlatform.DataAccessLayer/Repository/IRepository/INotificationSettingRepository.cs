
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using System.Linq.Expressions;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;

public interface INotificationSettingRepository : IRepository<NotificationSetting>
{
    Task<IEnumerable<UserNotificationSettingPreferenceVM>> GetUserListAsync(string columnName, Expression<Func<NotificationSetting, bool>> filter);
}
