using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class StoryRepository : Repository<Story>, IStoryRepository
{
    public StoryRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
}
