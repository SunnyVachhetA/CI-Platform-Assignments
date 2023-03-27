using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
public class StoryController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public StoryController(IServiceUnit serviceUnit)
    {
        _serviceUnit= serviceUnit;
    }
    public IActionResult Index()
    {
        return View("StoryListing");
    }

    public IActionResult ShareStory()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AddStory(long userId)
    {
        SingleUserMissionsVM userMissions = _serviceUnit.MissionApplicationService.GetSingleUserMission(userId);
        List<SingleUserMissionListVM> missionList = new();

        int size = userMissions.MissionTitle.Count();
        for (int i = 0; i < size; i++)
            missionList.Add( new SingleUserMissionListVM()
            {
                MissionId = userMissions.MissionId[i],
                Title = userMissions.MissionTitle[i]
            });

        ViewBag.UserMissionList = missionList;
        return View();
    }

    [HttpPost]
    public IActionResult AddStory(AddStoryVM addStory)
    {
        return View();
    }
}
