using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
namespace CIPlatform.DataAccessLayer.Repository;
public class CityRepository : Repository<City>, ICityRepository
{
    private readonly CIDbContext _dbContext;
    public CityRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}