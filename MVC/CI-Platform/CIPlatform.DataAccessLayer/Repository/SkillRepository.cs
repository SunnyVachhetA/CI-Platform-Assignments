using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class SkillRepository : Repository<Skill>, ISkillRepository
{
    private readonly CIDbContext _dbContext;
    public SkillRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public int DeleteSkill(short skillId)
    {
        var idParam = new SqlParameter("@id", skillId);
        return _dbContext.Database.ExecuteSqlRaw("DELETE FROM skill WHERE skill_id = @id", idParam);
    }

    public int UpdateSkillStatus(short skillId, byte status)
    {
        var idParam = new SqlParameter("@id", skillId);
        var statusParam = new SqlParameter("@status", status);
        return _dbContext.Database.ExecuteSqlRaw("UPDATE skill SET status = @status WHERE skill_id=@id", statusParam, idParam);
    }
}
