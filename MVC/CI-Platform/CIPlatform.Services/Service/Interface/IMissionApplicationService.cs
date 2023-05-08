using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Services.Service.Interface;
public interface IMissionApplicationService
{
    Task<List<RecentVolunteersVM>> FetchRecentVolunteers(long missionId);
    SingleUserMissionsVM GetSingleUserMission(long userId);
    IEnumerable<SingleMissionVM> LoadUserApprovedMissions(long id);
    void DeleteMissionApplication(long missionId, long userId);
    void SaveApplication(long missionId, long userId);
    IEnumerable<MissionApplicationVM> LoadApplications();
    Task UpdateApplicationStatus(long id, byte status);
    IEnumerable<MissionApplicationVM> SearchApplication(string searchKey);
    long UserMissionCount(long id, MissionTypeEnum type);
    Task<MissionApplicationVM> FetchUserMissionApplicationAsync(long id);
}
