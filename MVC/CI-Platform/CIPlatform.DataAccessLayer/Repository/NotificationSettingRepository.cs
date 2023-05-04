using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;

public class NotificationSettingRepository : Repository<NotificationSetting>, INotificationSettingRepository
{
    public NotificationSettingRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
}
