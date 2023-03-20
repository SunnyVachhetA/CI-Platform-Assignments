using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IMissionRatingRepository : IRepository<MissionRating>
{
    Task<(long count, byte rating)> CalculateAverageMissionRating(long missionId);
    void UpdateUserMissionRating(MissionRating missionRating);
}
