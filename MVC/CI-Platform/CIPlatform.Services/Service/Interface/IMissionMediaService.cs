using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service.Interface;
public interface IMissionMediaService
{
    Task StoreMissionMedia(IEnumerable<IFormFile> missionImages, string wwwRootPath, long entityMissionId);
    Task<IEnumerable<MediaVM>> ConvertMediaToMediaVM(ICollection<MissionMedium> missionMissionMedia);
    Task EditMissionMedia(long missionMissionId, IEnumerable<IFormFile> missionImages,
        IEnumerable<string> preloadedMediaList, string wwwRootPath);
    
}
