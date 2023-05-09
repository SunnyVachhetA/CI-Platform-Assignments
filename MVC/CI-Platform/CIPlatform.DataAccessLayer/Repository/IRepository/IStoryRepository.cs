
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.VMConstants;
using System.Linq.Expressions;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IStoryRepository : IRepository<Story>
{
    IEnumerable<Story> GetAllApprovedStories(Func<Story, bool> filter);
    IEnumerable<Story> GetAllStories();
    Story GetUserStoryDraft(Func<Story, bool> filter);
    void UpdateUserStoryStatus(Story entity, UserStoryStatus pending);
    void UpdateUserStory(Story entity);
    Story GetStoryDetails(Func<Story, bool> filter);
    void UpdateStoryView(long storyId, long storyViews);
    IEnumerable<Story> GetStoriesWithMissionAndUser();
    int UpdateStoryDeletionStatus(long storyId, byte status);
    Task<Story?> FetchStoryDetailsByIdAsync(Expression<Func<Story, bool>> filter);
}
