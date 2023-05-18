using CISkillMaster.DataAccessLayer.Abstract;
using CISkillMaster.DataAccessLayer.Data;
using CISkillMaster.DataAccessLayer.Extension;
using CISkillMaster.Entities.DataModels;
using CISkillMaster.Entities.DTO;
using CISkillMaster.Entities.Request;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CISkillMaster.DataAccessLayer.Implementation;

public class SkillRepository : Repository<Skill>, ISkillRepository
{

    #region Constructor
    public SkillRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
    #endregion

    #region Methods
    public async Task<(int totalSkillCount, IEnumerable<Skill> skills)> GetPaginatedSkills(PaginationQuery query)
    {
        Stopwatch timer = new Stopwatch();
        timer.Start();
        var count = await _dbSet.CountAsync();
        timer.Stop();

        Console.WriteLine(timer.ElapsedMilliseconds/1000.0);

        timer.Start();
        var result = await _dbSet.AsQueryable()
                                        .AsNoTracking()
                                        .OrderBy(skill => skill.Title)
                                        .ApplyPagination(query)
                                        .ToListAsync();
        timer.Stop();

        Console.WriteLine(timer.ElapsedMilliseconds/1000.0);
        return (count, result);
    }

    public async Task<(int filterCount, IEnumerable<Skill> skills)> FilterSkillAsync(PaginationQuery query, Expression<Func<Skill, bool>> expression)
    {
        var result = _dbSet.AsQueryable()
            .AsNoTracking()
            .Where(expression);

        int filterCount = await result.CountAsync();
        result = result.ApplyPagination(query);

        return (filterCount, result);
    }

    #endregion
}
