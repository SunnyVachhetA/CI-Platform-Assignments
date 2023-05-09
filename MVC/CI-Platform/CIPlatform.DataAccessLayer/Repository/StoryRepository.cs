﻿using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.VMConstants;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CIPlatform.DataAccessLayer.Repository;
public class StoryRepository : Repository<Story>, IStoryRepository
{
    private readonly CIDbContext _dbContext;
    public StoryRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext; 
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

    public Story GetStoryDetails(Func<Story, bool> filter)
    {
        var query = StoryWithMissionAndUserInformation()
            // ReSharper disable once PossibleUnintendedQueryableAsEnumerable
                                .Where(filter)
                                .FirstOrDefault();
        return query!;
    }

    public void UpdateStoryView(long storyId, long storyView)
    {
        var storyViewParam = new SqlParameter("@storyView", storyView);
        var storyIdParam = new SqlParameter("@storyId", storyId);

        _dbContext.Database.ExecuteSqlRaw("UPDATE story SET story_view = @storyView WHERE story_id = @storyId", storyViewParam, storyIdParam);
    }


    public IEnumerable<Story> GetStoriesWithMissionAndUser() =>
    dbSet
            .AsNoTracking()
            .Include(story => story.Mission)
            .Include(story => story.User)
            .Where(story => story.Status != 0)
            .AsEnumerable();
    

    public int UpdateStoryDeletionStatus(long storyId, byte status)
    {

        var statusParam = new SqlParameter("@status", status);
        var storyIdParam = new SqlParameter("@storyId", storyId);

        return _dbContext.Database.ExecuteSqlRaw("UPDATE story SET is_deleted = @status WHERE story_id = @storyId", statusParam, storyIdParam);
    }

    public async Task<Story?> FetchStoryDetailsByIdAsync(Expression<Func<Story, bool>> filter)
        => await dbSet
        .AsNoTracking()
        .Include(story => story.User)
        .Include(story => story.Mission)
        .FirstOrDefaultAsync(filter);
}
