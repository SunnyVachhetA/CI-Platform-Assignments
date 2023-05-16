using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Areas.Admin.Utilities;
using CIPlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;

[Area("Admin")]
[AdminAuthentication]
[ServiceFilter(typeof(AjaxErrorFilter))]
public class CommentController : Controller
{
    private readonly IServiceUnit _serviceUnit;

    public CommentController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }

    #region Methods

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IEnumerable<CommentAdminVM> comments = await _serviceUnit.CommentService.GetAllCommentsAdminAsync();
        return PartialView("_Comments", comments);
    }

    [HttpGet]
    public async Task<IActionResult> View(long commentId)
    {
        CommentAdminVM comment = await _serviceUnit.CommentService.LoadUserCommentAsync(commentId);
        return PartialView("_CommentView", comment);
    }

    [HttpPatch]
    public async Task<IActionResult> Approve(long id)
    {
        await _serviceUnit.CommentService.UpdateApprovalStatus(id, 1);
        await PushNotificationForComment(id);
        return NoContent();
    }

    [HttpPatch]
    public async Task<IActionResult> Decline(long id)
    {
        await _serviceUnit.CommentService.UpdateApprovalStatus(id, 2);
        await PushNotificationForComment(id);
        return NoContent();
    }

    [HttpPatch]
    public async Task<IActionResult> Delete(long id)
    {
        await _serviceUnit.CommentService.UpdateDeleteStatus(id, 1);
        return NoContent();
    }
    #endregion 


    #region Helper Methods

    private async Task PushNotificationForComment(long id)
    {
        CommentAdminVM comment = await _serviceUnit.CommentService.LoadUserCommentAsync(id);

        UserNotificationTemplate template = UserNotificationTemplate.ConvertFromComment(comment);
        string link = Url.Action("Index", "Mission", new { area = "Volunteer", id = comment.MissionId }, "https")!;
        
        template.Message = comment.ApprovalStatus == ApprovalStatus.APPROVED ?
           $"Your comment has been approved for mission : <a class='text-black-1' href='{link}'>{comment.MissionTitle}</a>" 
           : $"Your comment has been declined for mission : <a class='text-black-1' href='{link}'>{comment.MissionTitle}</a>";

        //bool isOpenForEmail = await _serviceUnit.PushNotificationService.PushNotificationToUserAsync(template);
        bool isOpenForEmail = await _serviceUnit.PushNotificationService.PushNotificationToUserSPAsync(template);
        if (isOpenForEmail)
            _ = _serviceUnit.PushNotificationService.PushEmailNotificationToUserAsync(template, link);
    }
    #endregion 
}
