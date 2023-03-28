using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
public class StoryController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public StoryController(IServiceUnit serviceUnit, IWebHostEnvironment webHostEnvironment)
    {
        _serviceUnit = serviceUnit;
        _webHostEnvironment = webHostEnvironment;   
    }
    public IActionResult Index()
    {
        IEnumerable< ShareStoryVM > storyList = new List<ShareStoryVM>();
        storyList = _serviceUnit.StoryService.FetchAllUserStories();

        return View("StoryListing", storyList);
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
        AddStoryVM userDraft = _serviceUnit.StoryService.FetchUserStoryDraft(userId, _webHostEnvironment.WebRootPath);
        if (userDraft != null)  return View("EditStory", userDraft);
        else return View();
    }

    [HttpPost]
    public IActionResult AddStory(AddStoryVM addStory, string storyAction)
    {
        if(ModelState.IsValid)
        {
            if (storyAction.Equals("share",StringComparison.OrdinalIgnoreCase))
            {
                addStory.StoryStatus = UserStoryStatus.PENDING;
            }
            else
            {
                addStory.StoryStatus = UserStoryStatus.DRAFT;
            }
            
            long storyID = _serviceUnit.StoryService.AddUserStory( addStory );

            //long storyID = _serviceUnit.StoryService.FetchStoryByUserAndMissionID( addStory.UserId, addStory.MissionID );

            if (storyID != 0)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                _serviceUnit.StoryMediaService.AddStoryMediaToUserStory(storyID, addStory.StoryMedia, wwwRootPath);
            }
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("Error", "Something Went Wrong!");
            return RedirectToAction("AddStory", "Story", new { userId = addStory.UserId });
        }
    }
}
