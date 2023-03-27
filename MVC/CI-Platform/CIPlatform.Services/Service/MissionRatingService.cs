using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class MissionRatingService : IMissionRatingService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionRatingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public static List<MissionRatingVM> ConvertMissionToRatingVM(Mission mission)
    {

        IEnumerable< MissionRatingVM > ratingVMList = mission.MissionRatings
                          .Select
                          ( missionRating => 
                                   new MissionRatingVM() 
                                   { 
                                       MissionId = missionRating.MissionId,
                                       UserId = missionRating.UserId,
                                       Rating = missionRating.Rating,
                                   } 
                          );

        return ratingVMList.ToList();
    }
    public Task SaveUserMissionRating(long missionId, long userId, byte rating)
    {
        _unitOfWork.MissionRatingRepo.Add
            (
                new()
                {
                    MissionId = missionId,
                    UserId = userId,    
                    Rating = rating,
                    CreatedAt = DateTimeOffset.Now
                }
            );

        _unitOfWork.Save();
        return Task.CompletedTask;
    }
    public async Task<(long volunteerCount, byte avgRating)> GetAverageMissionRating(long missionId)
    {
        (long count, byte rating) result = await _unitOfWork.MissionRatingRepo.CalculateAverageMissionRating(missionId);

        return result;
    }

    public Task UpdateUserMissionRating(MissionRatingVM ratingVm)
    {

        var missionRating = _unitOfWork.MissionRatingRepo.GetFirstOrDefault
            (
            (missionRating) =>
                ( missionRating.UserId == ratingVm.UserId && missionRating.MissionId == ratingVm.MissionId )
            );

        if (missionRating == null) { throw new NullReferenceException("Mission Rating Search Null Reference"); }

        missionRating.Rating = ratingVm.Rating ?? missionRating.Rating;
        _unitOfWork.MissionRatingRepo.UpdateUserMissionRating( missionRating );
        _unitOfWork.Save();
        return Task.CompletedTask;
    }
}
