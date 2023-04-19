using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class BannerRepository: Repository<Banner>, IBannerRepository
{
    public BannerRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
}
