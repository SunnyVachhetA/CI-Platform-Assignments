using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class MissionMediaRepository : Repository<MissionMedium>, IMissionMediaRepository
{
    private readonly CIDbContext _dbContext;
    public MissionMediaRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext; 
    }
}
