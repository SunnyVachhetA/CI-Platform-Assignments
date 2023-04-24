using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service;
public class MissionDocumentService : IMissionDocumentService
{
    private readonly IUnitOfWork _unitOfWork;
    public MissionDocumentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task StoreMissionDocument(IEnumerable<IFormFile> docs, string wwwRootPath,
        long missionId)
    {
        if (!docs.Any()) return;

        try
        {
            var docList =
                await StoreMediaService.StoreDocumentsToWebRootAsync(wwwRootPath, @"documents\mission", docs);
            var entities = docList.Select((doc) => ConvertVMToMissionDocument(doc, missionId));
            await _unitOfWork.MissionDocumentRepo.AddRangeAsync(entities);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during saving documents: " + e.Message);
            Console.WriteLine(e.StackTrace);
            throw;
        }

    }

    public static MissionDocument ConvertVMToMissionDocument(MissionDocumentVM doc, long missionId)
    {
        MissionDocument entity = new()
        {
            MissionId = missionId,
            DocumentName = doc.Name,
            DocumentPath = doc.Path,
            DocumentType = doc.Type,
            CreatedAt = DateTimeOffset.Now,
            DocumentTitle = doc.Title
        };
        return entity;
    }
}
