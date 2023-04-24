using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class MissionDocumentRepository: Repository<MissionDocument>, IMissionDocumentRepository
{
    public MissionDocumentRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
}
