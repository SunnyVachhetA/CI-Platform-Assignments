using CISkillMaster.Entities.DataModels;
using CISkillMaster.Entities.Request;
using System.Linq.Expressions;

namespace CISkillMaster.DataAccessLayer.Abstract;

public interface ISkillRepository : IRepository<Skill>
{
    Task<(int totalSkillCount, IEnumerable<Skill> skills)> GetPaginatedSkills(PaginationQuery query);
    Task<(int filterCount, IEnumerable<Skill> skills)> FilterSkillAsync(PaginationQuery query, Expression<Func<Skill, bool>> expression);
}
