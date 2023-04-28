using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IMissionRepository : IRepository<Mission>
{
    Mission FetchMissionDetailsById(long id);
    List<Mission> GetAllMissions();
    List<Mission> LoadFilteredMissions(FilterModel filterModel);
    IQueryable<Mission> FetchMissionInformation();
    void UpdateMissionRating(long missionId, byte avgRating);
    IEnumerable<Mission> FetchAllMissions();
    IEnumerable<Mission> FetchMissionCardInformation();
    Task<Mission> FetchMissionWithMedia(long id);
    Task<Mission> FetchMissionWithMediaGoal(long id);
    Task<IEnumerable<Mission>> FetchMissionCardInformationAsync();
    Task<int> UpdateMissionActiveStatus(long id, byte status);
    Task<int> UpdateMissionCloseStatus(long id, int status);
    void CloseGoalMission(long entryMissionId);
}
