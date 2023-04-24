using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service.Interface;
public interface IMissionMediaService
{
    Task StoreMissionMedia(IEnumerable<IFormFile> missionImages, string wwwRootPath, long entityMissionId);
}
