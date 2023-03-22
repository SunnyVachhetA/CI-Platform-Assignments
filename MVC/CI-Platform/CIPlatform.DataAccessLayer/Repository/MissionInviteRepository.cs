using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
namespace CIPlatform.DataAccessLayer.Repository;
public class MissionInviteRepository: Repository<MissionInvite>, IMissionInviteRepository
{
    public MissionInviteRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
}
