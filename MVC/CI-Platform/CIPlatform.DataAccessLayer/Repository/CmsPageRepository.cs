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
    
    public void UpdateCMSPage(short cmsId, byte status)
    {

        var idParam = new SqlParameter("@id", cmsId);
        var statusParam = new SqlParameter("@status", status);
        _dbContext.Database.ExecuteSqlRaw("UPDATE cms_page SET status = @status WHERE cms_page_id = @id", statusParam, idParam);
    }
}
