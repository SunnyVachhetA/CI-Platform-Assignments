using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IStoryService
{
    void AddUserStory(AddStoryVM addStory);
    IEnumerable<ShareStoryVM> FetchAllUserStories();
    long FetchStoryByUserAndMissionID(long userId, long missionID);
    AddStoryVM FetchUserStoryDraft(long userId, string wwwRootPath);
}
