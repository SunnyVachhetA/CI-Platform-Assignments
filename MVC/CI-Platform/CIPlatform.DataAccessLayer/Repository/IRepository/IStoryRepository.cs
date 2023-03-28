
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IStoryRepository : IRepository<Story>
{
    IEnumerable<Story> GetAllApprovedStories(Func<Story, bool> filter);
    IEnumerable<Story> GetAllStories();
    Story GetUserStoryDraft(Func<Story, bool> filter);
}
