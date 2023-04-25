
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service.Interface;
public interface IMissionDocumentService
{
    Task StoreMissionDocument(IEnumerable<IFormFile> missionImages, string wwwRootPath, long entityMissionId);
    Task<IEnumerable<MissionDocumentVM>> ConvertToDocumentVM(ICollection<MissionDocument> missionMissionDocuments);
}
