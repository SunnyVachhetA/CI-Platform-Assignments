using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class ThemeRepository : Repository<MissionTheme>, IThemeRepository
{
    private readonly CIDbContext _dbContext;
    public ThemeRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
