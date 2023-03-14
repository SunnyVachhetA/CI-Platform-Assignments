using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IMissionService
{
    List<MissionCardVM> FilterMissions(FilterModel filterModel);
    List<MissionCardVM> GetAllMissionCards();
    MissionCardVM LoadMissionDetails(long id);
    List<MissionCardVM> LoadRelatedMissionBasedOnTheme( int? themeId );
    MissionLandingVM CreateMissionLanding();

    MissionLandingVM CreateMissionLanding(List<MissionCardVM> missionList);
}
