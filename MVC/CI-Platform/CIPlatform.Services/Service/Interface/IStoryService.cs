using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IStoryService
{
    void AddUserStory(AddStoryVM addStory);
    long FetchStoryByUserAndMissionID(long userId, long missionID);
}
