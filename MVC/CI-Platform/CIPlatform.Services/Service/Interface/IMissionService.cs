﻿using CIPlatform.Entities.ViewModels;

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
}
