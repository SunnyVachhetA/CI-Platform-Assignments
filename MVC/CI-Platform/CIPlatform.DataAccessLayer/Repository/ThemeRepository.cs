using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class ThemeRepository : Repository<MissionTheme>, IThemeRepository
{
    private readonly CIDbContext _dbContext;
    public ThemeRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public int DeleteTheme(short themeId)
    {
        var idParam = new SqlParameter("@id", themeId);
        return _dbContext.Database.ExecuteSqlRaw("DELETE FROM mission_theme WHERE theme_id = @id", idParam);
    }

    public int DeActivateTheme(short themeId)
    {
        var idParam = new SqlParameter("@id", themeId);
        var statusParam = new SqlParameter("@status", false);
        return _dbContext.Database.ExecuteSqlRaw("UPDATE mission_theme SET status = @status WHERE theme_id=@id", statusParam, idParam);
    }
}
