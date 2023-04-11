using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class CmsPageRepository: Repository<CmsPage>, ICmsPageRepository
{
    public CmsPageRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
}
