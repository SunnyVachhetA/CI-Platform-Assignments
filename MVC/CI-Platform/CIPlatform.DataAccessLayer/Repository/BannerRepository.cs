using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class BannerRepository: Repository<Banner>, IBannerRepository
{
    private readonly CIDbContext _dbContext;
    public BannerRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public int UpdateBannerStatus(long bannerId, byte status)
    {
        var query = status == 0 ? "UPDATE banner SET status = {0}, deleted_at = {1} WHERE banner_id = {2}" 
                                    : 
                                        "UPDATE banner SET status = {0}, updated_at = {1} WHERE banner_id = {2}";

        return _dbContext.Database.ExecuteSqlRaw(query, status, DateTimeOffset.Now, bannerId);
    }
}
