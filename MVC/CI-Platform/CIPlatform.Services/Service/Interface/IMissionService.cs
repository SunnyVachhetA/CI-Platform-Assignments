using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IMissionService
{
    List<MissionCardVM> FilterMissions(FilterModel filterModel);
    List<MissionCardVM> GetAllMissionCards();
    MissionCardVM LoadMissionDetails(long id);
    List<MissionCardVM> LoadRelatedMissionBasedOnTheme( short themeId, long missionId );
    MissionLandingVM CreateMissionLanding();
    MissionLandingVM CreateMissionLanding(List<MissionCardVM> missionList);
    Task UpdateMissionRating(long missionId, byte avgRating);
    bool IsThemeUsedInMission(short themeId);
    (IEnumerable<MissionVMCard>, long) LoadAllMissionCards(int page);
    Task<(IEnumerable<MissionVMCard>, long)> FilterMissionsCard(FilterModel filterModel);
    IEnumerable<AdminMissionVM> LoadAllMissionsAdmin();
    IEnumerable<AdminMissionVM> SearchMission(string searchKey);
    Task CreateTimeMission(TimeMissionVM mission, string wwwRootPath);
    Task CreateGoalMission(GoalMissionVM mission, string wwwRootPath);
    Task<TimeMissionVM> LoadEditTimeMissionDetails(long id);
    Task UpdateTimeMission(TimeMissionVM mission, IEnumerable<string> preloadedMediaList,
        IEnumerable<string> preloadedDocumentPathList, IEnumerable<short> preloadedSkill, string wwwRootPath);

    Task<GoalMissionVM> LoadEditGoalMissionDetails(long id);
    Task UpdateGoalMission(GoalMissionVM mission, IEnumerable<string> preloadedMediaList, IEnumerable<string> preloadedDocumentPathList, IEnumerable<short> preloadedSkill, string wwwRootPath);
    Task<(IEnumerable<MissionVMCard>, long)> LoadAllMissionCardsAsync(int page);
    Task UpdateMissionActiveStatus(long id, byte status = 0);
    Task UpdateMissionCloseStatus(long id, int status);
}
