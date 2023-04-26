using System.Diagnostics.CodeAnalysis;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service;
[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
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

    public async Task<IEnumerable<MissionDocumentVM>> ConvertToDocumentVM(ICollection<MissionDocument> documents)
    {
        try
        {
            var docs = await Task.Run(() => documents.Select(ConvertToDocumentVM));
            return docs;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during loading documents: " + e.Message);
            Console.WriteLine(e.StackTrace);
            throw;
        }
    }

    public async Task EditMissionDocument(long missionId, IEnumerable<IFormFile> currentDoc, IEnumerable<string> preloadedDocumentPathList,
        string wwwRootPath)
    {
        try
        {
            if (!currentDoc.Any() && !preloadedDocumentPathList.Any()) return;
            
            var prevFileNames = preloadedDocumentPathList.Select(Path.GetFileName).ToList();
            
            if (!currentDoc.Any())
            {
                await DeleteDocument(missionId, prevFileNames!, wwwRootPath);
                return;
            }
            if (!preloadedDocumentPathList.Any())
            {
                await StoreMissionDocument(currentDoc, wwwRootPath, missionId);
                return;
            }

            var addFiles = new List<IFormFile>();
            foreach (var document in currentDoc)
            {
                var docName = Path.GetFileName(document.FileName);
                if( !preloadedDocumentPathList.Any(name => name.EqualsIgnoreCase(docName))) addFiles.Add(document);
            }
            await StoreMissionDocument(addFiles, wwwRootPath, missionId);

            var currentFileNames = currentDoc.Select(media => Path.GetFileName(media.FileName)).ToList();
            var deleteFiles = preloadedDocumentPathList.Except(currentFileNames);

            await DeleteDocument(missionId, deleteFiles, wwwRootPath);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during document edit: " + e.Message);
            Console.WriteLine(e.StackTrace);
            throw;
        }
    }

    private async Task DeleteDocument(long missionId, IEnumerable<string> files, string wwwRootPath)
    {
        string directory = $@"{wwwRootPath}\documents\mission\";

        foreach (var file in files)
        {
            string filePath = Path.Combine(directory, file);
            StoreMediaService.DeleteFileFromWebRoot(filePath);

            int result = await _unitOfWork.MissionDocumentRepo.DeleteDocument(missionId, file.Split(".")[0]);
            if (result == 0)
                throw new Exception("Error during mission document deletion!");
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

    public static MissionDocumentVM ConvertToDocumentVM(MissionDocument document)
    {
        var doc = new MissionDocumentVM()
        {
            Title = document.DocumentTitle?? string.Empty,
            Path = document.DocumentPath?? string.Empty,
            Type = document.DocumentType?? string.Empty,
            Name = document.DocumentName ?? string.Empty
        };
        return doc;
    }
}
