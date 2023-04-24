
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service.Interface;
public interface IMissionDocumentService
{
    Task StoreMissionDocument(IEnumerable<IFormFile> missionImages, string wwwRootPath, long entityMissionId);
}
