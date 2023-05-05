using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;

public class NotificationRepository : Repository<Notification>, INotificationRepository
{
    public NotificationRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
}
