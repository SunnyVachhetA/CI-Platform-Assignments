using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class MissionApplicationRepository : Repository<MissionApplication>, IMissionApplicationRepository
{
    private readonly CIDbContext _dbContext;
    public MissionApplicationRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext; 
    }
}
