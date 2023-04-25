using CIPlatform.DataAccessLayer.Repository;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service;
public class MissionMediaService : IMissionMediaService
{
    private IUnitOfWork _unitOfWork;

    public MissionMediaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task StoreMissionMedia(IEnumerable<IFormFile> missionImages, string wwwRootPath, long missionId)
    {

        try
        {
            var mediaList = await StoreMediaService.StoreMediasToWwwRootAsync(wwwRootPath, @"images\mission", missionImages);
            var entities = mediaList
                .Select((media) => ConvertToMissionMedia(media, missionId));
            await _unitOfWork.MissionMediaRepo.AddRangeAsync(entities);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during store media: " + e.Message);
            Console.WriteLine(e.StackTrace);
            throw;
        }

    }

    public async Task<IEnumerable<MediaVM>> ConvertMediaToMediaVM(ICollection<MissionMedium> media)
    {
        try
        {
            IEnumerable<MediaVM> mediaList = await
                Task.Run(() =>
                {
                    return media.Select((file) => new MediaVM()
                    {
                        Path = file.MediaPath!,
                        Type = file.MediaType!,
                        Name = file.MediaName!
                    });
                });
            return mediaList;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during get media: " + e.Message);
            Console.WriteLine(e.StackTrace);
            throw;
        }
    }

    public async Task EditMissionMedia(long missionId, IEnumerable<IFormFile> currentMedia,
        IEnumerable<string> preloadedMediaList, string wwwRootPath)
    {
        var prevFileNames = preloadedMediaList.Select(Path.GetFileName).ToList();
        var currentFileNames = new List<string>();
        var addFiles = new List<IFormFile>();
        foreach (var media in currentMedia)
        {
            var fileName = Path.GetFileName(media.FileName);
            currentFileNames.Add(fileName);
            if (!prevFileNames.Any(name => name!.EqualsIgnoreCase(fileName)))
            {
                addFiles.Add(media);
            }
        }

        var mediaList = await StoreMediaService.StoreMediasToWwwRootAsync(wwwRootPath, @"images\mission", addFiles);

        var entities = mediaList.Select(img => ConvertToMissionMedia(img, missionId));

        await _unitOfWork.MissionMediaRepo.AddRangeAsync(entities);

        var deleteFiles = prevFileNames.Except(currentFileNames);
        string directory = $@"{wwwRootPath}\images\mission\";

        foreach (var file in deleteFiles)
        {
            StoreMediaService.DeleteFileFromWebRoot(Path.Combine(directory, file));

            await _unitOfWork.MissionMediaRepo.DeleteMedia(missionId, file.Split(".")[0]);
        }
    }

    public static MissionMedium ConvertToMissionMedia(MediaVM media, long missionId)
    {
        MissionMedium entity = new()
        {
            MissionId = missionId,
            MediaName = media.Name,
            MediaType = media.Type,
            MediaPath = media.Path,
            CreatedAt = DateTimeOffset.Now,
            Default = true
        };
        return entity;
    }

}
