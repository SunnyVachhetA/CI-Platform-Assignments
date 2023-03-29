using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service;
public class StoryMediaService : IStoryMediaService
{
    private readonly IUnitOfWork _unitOfWork;

    public StoryMediaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddStoryMediaToUserStory(long storyID, List<IFormFile> storyMedia, string wwwRootPath)
    {
        List<MediaVM> filePathList = StoreMediaService.storeMediaToWWWRoot(wwwRootPath, @"images\story", storyMedia);

        List<StoryMedium> storyMediaList = filePathList
                                            .Select
                                            ( media => 
                                                ConvertPathToStoryMediaModel
                                                (
                                                    storyID, media.Path, media.Type, media.Name
                                                ) 
                                            ).ToList();

        _unitOfWork.StoryMediaRepo.AddRange( storyMediaList );
        _unitOfWork.Save();
    }

    public StoryMedium ConvertPathToStoryMediaModel( long storyID, string path, string type, string name )
    {
        StoryMedium storyMedium = new()
        {
            StoryId = storyID,
            MediaName = name,
            MediaType = type,
            MediaPath = path,
            CreatedAt = DateTimeOffset.Now
        };

        return storyMedium;
    }

    public void DeleteStoryMedia(long storyId, string file)
    {
        string fileName = file.Split(".")[0];

        var entity = _unitOfWork.StoryMediaRepo.GetFirstOrDefault( media => media.StoryId == storyId && media.MediaName!.Equals(fileName) );

        _unitOfWork.StoryMediaRepo.Remove( entity );

        _unitOfWork.Save();
    }

    public void DeleteAllStoryMedia(long storyId)
    {
        var storyMediaList = _unitOfWork
                                                    .StoryMediaRepo
                                                    .GetAll
                                                     (
                                                         media => media.StoryId == storyId
                                                     );
        _unitOfWork.StoryMediaRepo.RemoveRange(storyMediaList);
        _unitOfWork.Save();
    }

    
}
