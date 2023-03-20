using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IMissionRatingService
{
    Task<(long volunteerCount, byte avgRating)> GetAverageMissionRating(long missionId);
    Task SaveUserMissionRating(long missionId, long userId, byte rating);
    Task UpdateUserMissionRating(MissionRatingVM ratingVm);
}
