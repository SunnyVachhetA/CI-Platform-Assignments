using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;

[Area("Admin")]
[AdminAuthentication]
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
    public async Task<IActionResult> DeActivate(long storyId)
    {
        try
        {
            _serviceUnit.StoryService.UpdateStoryDeletionStatus(storyId, 1);
            await PushStoryApprovalNotification(storyId);
            return NoContent();
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
            //var stories = _serviceUnit.StoryService.LoadAllStoriesAdmin();
            //return PartialView("_Stories", stories);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during story De-activation: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    
    [HttpPatch]
    public async Task<IActionResult> ApproveStory(long storyId)
    {
        try
        {
            _serviceUnit.StoryService.UpdateUserStoryStatus(storyId, UserStoryStatus.APPROVED);
            await PushStoryApprovalNotification(storyId);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during story De-activation: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> DeclineStory(long storyId)
    {
        try
        {
            _serviceUnit.StoryService.UpdateUserStoryStatus(storyId, UserStoryStatus.DECLINED);
            await PushStoryApprovalNotification(storyId);
            return NoContent();
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

    #region HelperMethods

    public async Task PushStoryApprovalNotification(long storyId)
    {
        AdminStoryVM vm = await _serviceUnit.StoryService.FetchUserStoryDetailsAsync(storyId);

        if (vm is null || vm.StoryStatus == UserStoryStatus.PENDING) return;

        UserNotificationTemplate template = UserNotificationTemplate.ConvertFromStory(vm);

        string link = template.Type == NotificationTypeEnum.APPROVE ?
                Url.Action("Story", "Story", new { area = "Volunteer", id = storyId }, "https")!
                : Url.Action("Index", "Story", new { area = "Admin" }, "https")!;

        template.Message = template.Type == NotificationTypeEnum.APPROVE ?
            $"Story request has been approved for <a href='{link}'>{template.Title}</a>"
            : $"Story request has been declined for {template.Title}";


        bool isOpenForEmail = await _serviceUnit.PushNotificationService.PushNotificationToUserAsync(template);
        if (isOpenForEmail)
            _ = _serviceUnit.PushNotificationService.PushEmailNotificationToUserAsync(template, link);
        
    }

    #endregion
}
