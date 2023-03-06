using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class CountryRepository: Repository<Country>, ICountryRepository
{
    private readonly CIDbContext _dbContext;
    public CountryRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
