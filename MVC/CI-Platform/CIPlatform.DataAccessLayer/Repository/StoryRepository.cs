using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.VMConstants;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class StoryRepository : Repository<Story>, IStoryRepository
{
    public StoryRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
    
    /// <summary>
    /// This method joins story table with user, story media, mission and then on theme.
    /// </summary>
    /// <returns>IQueryable for further query.</returns>
    private IQueryable<Story> StoryWithMissionAndUserInformation()
    {
        var query = dbSet
                    .Include(story => story.StoryMedia)
                    .Include(story => story.User)
                    .Include(story => story.Mission)
                        .ThenInclude(msn => msn.Theme);
        return query;
    }

    /// <summary>
    /// Loads all story from DB
    /// </summary>
    /// <returns>IEnumerable of story</returns>
    public IEnumerable<Story> GetAllStories()
    {
        var query = StoryWithMissionAndUserInformation();

        return query.AsEnumerable();
    }

    /// <summary>
    /// Fetches approved user stories from DB
    /// </summary>
    /// <param name="filter">Func that takes story and return bool</param>
    /// <returns>Filtered story list</returns>
    public IEnumerable<Story> GetAllApprovedStories(Func<Story, bool> filter)
    {
        var query = StoryWithMissionAndUserInformation();

        IEnumerable<Story> stories = new List<Story>(); 
        if(query.Any())
        {
            stories = query.Where(filter);
        }
        return stories;
    }


    /// <summary>
    /// Finds users first story draft 
    /// </summary>
    /// <param name="filter">filter by user id and status</param>
    /// <returns>story draft</returns>
    public Story GetUserStoryDraft(Func<Story, bool> filter)

    {
        var query = StoryWithMissionAndUserInformation()
                    .Where(filter);

        if (query.Any())
            return query.First();
        else
            return null!;
    }

    public void UpdateUserStoryStatus(Story entity, UserStoryStatus pending)
    {
        entity.Status = (byte)pending;

        dbSet.Update(entity);
    }

    public void UpdateUserStory(Story entity)
    {
        dbSet.Update(entity);
    }
}
