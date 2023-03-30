using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class StoryInviteRepository: Repository<StoryInvite>, IStoryInviteRepository
{
    public StoryInviteRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
}
