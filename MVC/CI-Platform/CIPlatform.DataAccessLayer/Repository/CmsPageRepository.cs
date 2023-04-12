using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class CmsPageRepository: Repository<CmsPage>, ICmsPageRepository
{
    private readonly CIDbContext _dbContext;
    public CmsPageRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void DeleteCMSPageSQL(short cmsId)
    {
        var idParam = new SqlParameter("@id", cmsId);
        _dbContext.Database.ExecuteSqlRaw("DELETE FROM cms_page WHERE cms_page_id = @id", idParam);
    }
}
