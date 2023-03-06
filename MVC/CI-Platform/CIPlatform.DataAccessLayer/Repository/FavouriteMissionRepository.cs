using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class FavouriteMissionRepository : Repository<FavouriteMission>, IFavouriteMissionRepository
{
    private readonly CIDbContext _dbContext;
    public FavouriteMissionRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
