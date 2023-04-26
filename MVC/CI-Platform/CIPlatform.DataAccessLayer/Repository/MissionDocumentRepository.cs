using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class MissionDocumentRepository: Repository<MissionDocument>, IMissionDocumentRepository
{
    private readonly CIDbContext _dbContext;
    public MissionDocumentRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> DeleteDocument(long missionId, string name)
    {
        var query = "DELETE FROM mission_document WHERE document_name = {0} AND mission_id = {1}";

        return await _dbContext.Database.ExecuteSqlRawAsync(query, name, missionId);
    }
}
