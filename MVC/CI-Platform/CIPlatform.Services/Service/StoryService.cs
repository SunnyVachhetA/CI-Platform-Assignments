using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class StoryService : IStoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public StoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    //Adding user story based on story status 0 - Draft; 1 - Pending
    public void AddUserStory(AddStoryVM addStory)
    {
        Story story = new()
        {
            UserId = addStory.UserId,
            MissionId = addStory.MissionID,
            Title = addStory.Title,
            Description = addStory.Description,
            Status = (byte?)addStory.StoryStatus,
            CreatedAt = addStory.CreatedAt
        };
        _unitOfWork.StoryRepo.Add( story );
        _unitOfWork.Save();
    }

    public long FetchStoryByUserAndMissionID(long userId, long missionID)
    {
        Story story = _unitOfWork
                        .StoryRepo
                        .GetFirstOrDefault( (story) => (story.UserId == userId && story.MissionId == missionID) );

        if(story != null) return story.StoryId;
        return 0;
    }
}
