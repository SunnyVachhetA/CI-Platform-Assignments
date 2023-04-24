using CIPlatform.DataAccessLayer.Repository;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
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
