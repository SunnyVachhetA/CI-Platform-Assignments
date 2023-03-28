using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service;
public class StoryService : IStoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public StoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    //Adding user story based on story status 0 - Draft; 1 - Pending
    public long AddUserStory(AddStoryVM addStory)
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
        return story.StoryId;
    }


    public long FetchStoryByUserAndMissionID(long userId, long missionID)
    {
        Story story = _unitOfWork
                        .StoryRepo
                        .GetFirstOrDefault( (story) => (story.UserId == userId && story.MissionId == missionID) );

        if(story != null) return story.StoryId;
        return 0;
    }
    
    //Fetches all user stories from db and convert
    public IEnumerable<ShareStoryVM> FetchAllUserStories()
    {
        Func<Story, bool> filter = (story) => story.Status == (byte)UserStoryStatus.APPROVED;
        IEnumerable<Story> stories = _unitOfWork.StoryRepo.GetAllApprovedStories(filter);

        IEnumerable<ShareStoryVM> shareStoryList = new List< ShareStoryVM >();
        if( stories.LongCount() > 0 )
        {
            shareStoryList = stories.Select((story) => ConvertStoryModelToShareStoryVM(story));
        }
        return shareStoryList;
    }

    /// <summary>
    /// Converts Story data model into share story VM
    /// </summary>
    /// <param name="story">story data model object as parameter</param>
    /// <returns>Share story view model object</returns>
    public ShareStoryVM ConvertStoryModelToShareStoryVM(Story story)
    {
        ShareStoryVM shareStory = new()
        {
            StoryId= story.StoryId,
            UserId = story.UserId,
            MissionId = story.MissionId,    
            StoryTitle = story.Title!,
            ThemeName = story.Mission?.Theme?.Title ?? string.Empty,
            Description = story.Description!,
            StoryThumbnail = GetStoryThumbnail( story.StoryMedia ),
            UserAvatar = story.User.Avatar ?? string.Empty,
            UserName = story.User.FirstName + " " + story.User.LastName,
        };

        return shareStory;
    }
    private string GetStoryThumbnail(ICollection<StoryMedium> storyMedia)
    {
        StoryMedium? media = storyMedia.FirstOrDefault();    
        
        if(media == null) return string.Empty;

        string path = $@"{media.MediaPath}{media.MediaName}{media.MediaType}";
        return path;
    }

    public AddStoryVM FetchUserStoryDraft(long userId, string wwwRootPath)
    {
        Func<Story, bool> filter = (story) => (story.UserId == userId && story.Status == 0);   
        Story story = _unitOfWork.StoryRepo.GetUserStoryDraft( filter );

        if (story == null) return null!;

        AddStoryVM storyVM = ConvertStoryToAddStoryVM(story, wwwRootPath);
        return storyVM;
    }

    public AddStoryVM ConvertStoryToAddStoryVM(Story story, string wwwRootPath)
    {
        AddStoryVM storyVM = new()
        {
            StoryId = story.StoryId,
            //StoryMedia = ConvertMediaPathToImageMedia(story.StoryMedia, wwwRootPath),
            Images = GetStoryMediaVM(story.StoryMedia),
            StoryStatus = UserStoryStatus.DRAFT,
            UserId = story.UserId,
            Title = story.Title?? string.Empty,
            MissionID = story.MissionId,
            Description = story.Description?? string.Empty,
            VideoUrl = story.VideoUrl?? string.Empty
        };

        return storyVM;
    }

    private List<MediaVM> GetStoryMediaVM(ICollection<StoryMedium> storyMedia)
    {
        if (!storyMedia.Any()) return new List< MediaVM >();

        List<MediaVM> mediaList = new();

        mediaList = storyMedia
                    .Select
                    (
                        media =>
                            new MediaVM()
                            {
                                Name = media.MediaName!,
                                Type = media.MediaType!,
                                Path = media.MediaPath!
                            }
                    ).ToList();

        return mediaList;
    }

    private List<IFormFile> ConvertMediaPathToImageMedia(ICollection<StoryMedium> storyMedia, string wwwRootPath)
    {
        if( !storyMedia.Any() ) { return new List<IFormFile>(); } 

        List<MediaVM> mediaList = storyMedia
                                    .Select
                                    (
                                        media =>
                                            new MediaVM()
                                            {
                                                Type = media.MediaType!,
                                                Name = media.MediaName!,
                                                Path = media.MediaPath!,
                                            }
                                    ).ToList(); 
        var result = StoreMediaService.FetchMediaFromRootPath( mediaList, wwwRootPath );
        return result;
    }
}
