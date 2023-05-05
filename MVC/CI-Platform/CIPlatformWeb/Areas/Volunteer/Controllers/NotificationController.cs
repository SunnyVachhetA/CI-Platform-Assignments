using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Areas.Admin.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Volunteer.Controllers;
[Area("Volunteer")]
[ServiceFilter(typeof(AjaxErrorFilter))]
public class NotificationController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public NotificationController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }

    [HttpGet]
    public async Task<IActionResult> Index(long id)
    {
        if (id <= 0) return NotFound();
        Console.WriteLine(DateTimeOffset.Now);
        NotificationContainerVM vm = await _serviceUnit.UserNotificationService.LoadAllNotificationsAsync(id);
        return PartialView("_Notifications", vm);
    }

    [HttpPut]
    public async Task<IActionResult> Settings(NotificationSettingVM vm)
    {
        await _serviceUnit.NotificationSettingService.UpdateNotificationSettingsAsync(vm);
        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> MarkAsRead(long userId, long id)
    {
        if(userId <= 0 || id <= 0) return BadRequest();

        await _serviceUnit.UserNotificationService.MarkNotificationAsReadAsync(userId, id);

        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete(long userId)
    {
        if (userId <= 0) return BadRequest();
        _ = _serviceUnit.UserNotificationService.DeleteAllNotification(userId);
        return NoContent();
    }

    [HttpPatch]
    public IActionResult UpdateLastCheck(long userId)
    {
        _ = _serviceUnit.UserNotificationCheckService.UpdateLastCheckAsync(userId);
        return Ok();
    }
}
