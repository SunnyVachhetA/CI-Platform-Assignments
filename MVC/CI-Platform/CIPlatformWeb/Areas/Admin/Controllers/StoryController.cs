using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class StoryController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public StoryController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }

    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<AdminStoryVM> stories = _serviceUnit.StoryService.LoadAllStoriesAdmin();
        return PartialView("_Stories", stories);
    }

    [HttpGet]
    public IActionResult Search(string searchKey)
    {
        try
        {
            var stories = _serviceUnit.StoryService.SearchByKey(searchKey);
            return PartialView("_Stories", stories);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during story search: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    
    [HttpPatch]
    public IActionResult DeActivate(long storyId)
    {
        try
        {
            _serviceUnit.StoryService.UpdateStoryDeletionStatus(storyId, 1);
            var stories = _serviceUnit.StoryService.LoadAllStoriesAdmin();
            return PartialView("_Stories", stories);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during story De-activation: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    [HttpPatch]
    public IActionResult Restore(long storyId)
    {
        try
        {
            _serviceUnit.StoryService.UpdateStoryDeletionStatus(storyId);
            var stories = _serviceUnit.StoryService.LoadAllStoriesAdmin();
            return PartialView("_Stories", stories);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during story De-activation: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    [HttpPatch]
    public IActionResult ApproveStory(long storyId)
    {
        try
        {
            _serviceUnit.StoryService.UpdateUserStoryStatus(storyId, UserStoryStatus.APPROVED);
            var stories = _serviceUnit.StoryService.LoadAllStoriesAdmin();
            return PartialView("_Stories", stories);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during story De-activation: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    [HttpPatch]
    public IActionResult DeclineStory(long storyId)
    {
        try
        {
            _serviceUnit.StoryService.UpdateUserStoryStatus(storyId, UserStoryStatus.DECLINED);
            var stories = _serviceUnit.StoryService.LoadAllStoriesAdmin();
            return PartialView("_Stories", stories);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during story De-activation: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult StoryDetails(long id)
    {
        ShareStoryVM vm = _serviceUnit.StoryService.LoadStoryDetails(id);
        return View(vm);
    }
}
