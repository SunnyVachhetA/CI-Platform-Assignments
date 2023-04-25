using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class MissionMediaRepository : Repository<MissionMedium>, IMissionMediaRepository
{
    private readonly CIDbContext _dbContext;
    public MissionMediaRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext; 
    }

    public async Task DeleteMedia(long missionId, string name)
    {
        string query = "DELETE from mission_media WHERE mission_id = {0} AND media_name = {1}";
        await _dbContext.Database.ExecuteSqlRawAsync(query, missionId, name);
    }
}
