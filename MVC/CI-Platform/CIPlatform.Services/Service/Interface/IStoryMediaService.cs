using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service.Interface;
public interface IStoryMediaService
{
    void AddStoryMediaToUserStory(long storyID, List<IFormFile> storyMedia, string wwwRootPath);
}
