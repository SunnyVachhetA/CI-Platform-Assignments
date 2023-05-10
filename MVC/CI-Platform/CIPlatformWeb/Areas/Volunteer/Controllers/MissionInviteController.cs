using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
[Authentication]
public class MissionInviteController : Controller
{
    private readonly IServiceUnit _serviceUnit;

    public MissionInviteController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }

    [HttpGet]
    [Route("/Volunteer/MissionInvite/MissionUsersInviteAsync")]
    public async Task<IActionResult> MissionUsersInviteAsync(long userId, long missionId)
    {
        var userList = await _serviceUnit.UserService.LoadAllActiveUserForRecommendMissionAsync(userId);

        var inviteList = _serviceUnit.MissionInviteService.fetchUserMissionInvites(userList, userId, missionId);

        return PartialView("_RecommendMission", inviteList);
    }

    [HttpPost]
    [Route("/Volunteer/MissionInvite/SendMissionInviteAsync")]
    public async Task<IActionResult> SendMissionInvitesAsync(long userId, long missionId, long[] recommendList, string missionTitle)
    {
        _ = await _serviceUnit.MissionInviteService.SaveMissionInviteFromUser(userId, missionId, recommendList);
        UserRegistrationVM user = await _serviceUnit.UserService.LoadUserBasicInformationAsync(userId);
        IEnumerable<UserNotificationSettingPreferenceVM> userPrefrence = await _serviceUnit.MissionInviteService.LoadUserMissionInvitePreference(userId, missionId, recommendList);

        string message = $"{user.FirstName} {user.LastName} - has recommend you this mission: {missionTitle}";
        await _serviceUnit.PushNotificationService.PushRecommendNotificationToUsersAsync(message, userPrefrence, user.Avatar, NotificationTypeEnum.RECOMMEND, NotificationTypeMenu.RECOMMEND_MISSION);
        string missionInviteLink = Url.Action("Index", "Mission", new { area = "Volunteer", id = missionId }, "https") ?? string.Empty;

        _ = _serviceUnit.PushNotificationService.PushRecommendEmailNotificationToUsersAsync(userPrefrence, missionTitle, missionInviteLink, $"{user.FirstName} {user.LastName}", NotificationTypeMenu.RECOMMEND_MISSION);

        return StatusCode(201);
    }
}