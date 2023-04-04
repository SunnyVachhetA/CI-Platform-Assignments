using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class UserSkillRepository: Repository<UserSkill>, IUserSkillRepository
{
    private readonly CIDbContext _dbContext;
    public UserSkillRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void DeleteFromUserSkill(short skillId, long userId)
    {
        var userParam = new SqlParameter("@userId", userId);
        var skillParam = new SqlParameter("@skillId", skillId);

        _dbContext.Database.ExecuteSqlRaw("DELETE FROM user_skill WHERE user_id = @userId AND skill_id = @skillId", userParam, skillParam);
    }
}
