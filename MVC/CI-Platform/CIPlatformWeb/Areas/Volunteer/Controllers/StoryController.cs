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
    public IActionResult Index(bool? missionError)
    {
        IEnumerable< ShareStoryVM > storyList = new List<ShareStoryVM>();
        storyList = _serviceUnit.StoryService.FetchAllUserStories();

        ViewBag.MissionError = missionError ?? false;

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

        if (!userMissions.MissionId.Any()) return RedirectToAction("Index", new { missionError = true });

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
        if (userDraft != null) 
        {
            List<string> mediaList = userDraft
                .Images
                .Select( image => image.Name+image.Type  )
                .ToList();
            ViewBag.MediaList = mediaList;  
            return View("EditStory", userDraft); 
        }
        else return View();
    }

    [HttpPost]
    public IActionResult AddStory(AddStoryVM addStory, string storyAction)
    {
        if(ModelState.IsValid)
        {
            addStory.StoryStatus = storyAction.Equals("share",StringComparison.OrdinalIgnoreCase) ? UserStoryStatus.PENDING : UserStoryStatus.DRAFT;
            
            long storyID = _serviceUnit.StoryService.AddUserStory( addStory );

            //long storyID = _serviceUnit.StoryService.FetchStoryByUserAndMissionID( addStory.UserId, addStory.MissionID );

            if (storyID != 0)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                _serviceUnit.StoryMediaService.AddStoryMediaToUserStory(storyID, addStory.StoryMedia, wwwRootPath);
            }

            if (addStory.StoryStatus == UserStoryStatus.PENDING) TempData["story-submit"] = "Story sent to admin for approval!";
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("Error", "Something Went Wrong!");
            return RedirectToAction("AddStory", "Story", new { userId = addStory.UserId });
        }
    }


    [HttpPost]
    public IActionResult EditStory(AddStoryVM editStory, string storyAction, List<string> preloadedMediaList)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("EditStory", editStory);
            
            editStory.StoryStatus = storyAction.Equals("share", StringComparison.OrdinalIgnoreCase) ? UserStoryStatus.PENDING : UserStoryStatus.DRAFT;

            _serviceUnit.StoryService.UpdateUserStory(editStory);

            string wwwRootPath = _webHostEnvironment.WebRootPath;

            string directoryPath = @$"{wwwRootPath}\images\story\";

            foreach(var file in preloadedMediaList)
            {
                bool isExists = editStory.StoryMedia.Any( media => ConvertMediaName(media.FileName) == file);
                if (!isExists) 
                {
                    DeleteFileFromWebRoot( Path.Combine(directoryPath, file) );
                    _serviceUnit.StoryMediaService.DeleteStoryMedia(editStory.StoryId, file);
                }
            }

            List<IFormFile> formFiles= new();

            foreach( var file in editStory.StoryMedia )
            {
                string fileName = ConvertMediaName(file.FileName);

                if (!preloadedMediaList.Contains(fileName))
                    formFiles.Add(file);
            }

            if(formFiles.Any())
            {
                _serviceUnit.StoryMediaService.AddStoryMediaToUserStory(editStory.StoryId, formFiles, wwwRootPath);
            }

            if (editStory.StoryStatus == UserStoryStatus.PENDING)
                TempData["story-submit"] = "Story sent to admin for approval!";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during edit story[post] : " + e.Message);
            Console.WriteLine(e.StackTrace);
            return NotFound();
        }
    }


    [HttpDelete]
    public IActionResult RemoveStory(long storyId)
    {
        if (storyId == 0) return BadRequest();
        try
        {
            _serviceUnit.StoryMediaService.DeleteAllStoryMedia( storyId );
            _serviceUnit.StoryService.DeleteStory(storyId);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured in remove story: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }


    [HttpGet]
    public IActionResult Story(long id)
    {
        ShareStoryVM storyVm = _serviceUnit.StoryService.LoadStoryDetails(id);
        _serviceUnit.StoryService.UpdateStoryView(storyVm.StoryId, storyVm.StoryViews);
        storyVm.StoryViews++;
        return View(storyVm);
    }

    private void DeleteFileFromWebRoot(string filePath)
    {
        if(System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
    }

    private string ConvertMediaName(string fileName)
    {
        if (fileName.Contains("\\"))
            return fileName.Split("\\")[^1];
        return fileName;
    }

    //Story Invite
    public IActionResult StoryUsersInvite(long userId, long storyId)
    {
        return PartialView("_RecommendMission");
    }

}
