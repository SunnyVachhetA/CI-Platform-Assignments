using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Services.Service.Interface;
public interface IStoryService
{
    long AddUserStory(AddStoryVM addStory);
    IEnumerable<ShareStoryVM> FetchAllUserStories();
    long FetchStoryByUserAndMissionID(long userId, long missionID);
    AddStoryVM FetchUserStoryDraft(long userId, string wwwRootPath);
    void UpdateUserStoryStatus(long storyId, UserStoryStatus pending);
    void DeleteStory(long storyId);
    void UpdateUserStory(AddStoryVM editStory);
    ShareStoryVM LoadStoryDetails(long id);
    void UpdateStoryView(long storyId, long storyVmStoryViews);
}
