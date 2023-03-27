using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using System;
namespace CIPlatform.DataAccessLayer.Repository;
public class StoryMediaRepository : Repository<StoryMedium>, IStoryMediaRepository
{
    public StoryMediaRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
}
