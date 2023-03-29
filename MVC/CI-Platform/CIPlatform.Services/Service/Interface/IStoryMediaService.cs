using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service.Interface;
public interface IStoryMediaService
{
    void AddStoryMediaToUserStory(long storyID, List<IFormFile> storyMedia, string wwwRootPath);
    void DeleteStoryMedia(long storyId, string file);
    void DeleteAllStoryMedia(long storyId);
}
