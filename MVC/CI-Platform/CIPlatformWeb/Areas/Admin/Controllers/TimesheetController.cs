using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;

[Area("Admin")]
[AdminAuthentication]
public class TimesheetController : Controller
{
    private readonly IServiceUnit _serviceUnit;

    public TimesheetController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }

    #region Methods
    [HttpGet]
    public IActionResult LoadHourTimesheet()
    {
        try
        {
            IEnumerable<VolunteerTimesheetVM> timesheet =
                _serviceUnit.TimesheetService.LoadHourTimesheet(MissionTypeEnum.TIME);
            return PartialView("_TimesheetHour", timesheet);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during Hour timesheet load: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult LoadGoalTimesheet()
    {
        try
        {
            IEnumerable<VolunteerTimesheetVM> timesheet =
                _serviceUnit.TimesheetService.LoadHourTimesheet(MissionTypeEnum.GOAL);
            return PartialView("_TimesheetGoal", timesheet);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during goal timesheet load: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> Approve(long timesheetId, string type)
    {
        try
        {
            _serviceUnit.TimesheetService.UpdateTimesheetStatus(timesheetId, 1, type);
            await PushTimesheetNotificationAsync(timesheetId);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during approve timesheet: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> Decline(long timesheetId, string? type)
    {
        try
        {
            _serviceUnit.TimesheetService.UpdateTimesheetStatus(timesheetId, 2, string.Empty);
            await PushTimesheetNotificationAsync(timesheetId);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during approve timesheet: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult SearchHour(string searchKey)
    {
        try
        {
            var timesheet = _serviceUnit.TimesheetService.SearchTimesheet(searchKey, MissionTypeEnum.TIME);
            return PartialView("_TimesheetHour", timesheet);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during search timesheet: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    [HttpGet]
    public IActionResult SearchGoal(string searchKey)
    {
        try
        {
            var timesheet = _serviceUnit.TimesheetService.SearchTimesheet(searchKey, MissionTypeEnum.GOAL);
            return PartialView("_TimesheetGoal", timesheet);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during search timesheet: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult View(long timesheetId)
    {
        try
        {
            if (timesheetId == 0) return BadRequest();

            VolunteerTimesheetVM entry = _serviceUnit.TimesheetService.ViewTimesheetEntry(timesheetId);
            return PartialView("_ViewTimesheetEntry", entry);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during view timesheet: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    #endregion

    #region Helper Methods
    private async Task PushTimesheetNotificationAsync(long timesheetId)
    {
        VolunteerTimesheetVM vm = await _serviceUnit.TimesheetService.ViewTimesheetEntryAsync(timesheetId);

        UserNotificationTemplate template = UserNotificationTemplate.ConvertFromTimesheet(vm);
        string link = Url.Action("VolunteerTimesheet", "User", new { area = "Volunteer", id = template.UserId }, "https")!;
        template.Message = GetTimesheetMessage(vm, link);
        //var isOpenForEmail = await _serviceUnit.PushNotificationService.PushNotificationToUserAsync(template);
        var isOpenForEmail = await _serviceUnit.PushNotificationService.PushNotificationToUserSPAsync(template);
        if (isOpenForEmail)
            _ = _serviceUnit.PushNotificationService.PushEmailNotificationToUserAsync(template, link);
    }

    private string GetTimesheetMessage(VolunteerTimesheetVM timesheet, string link)
    {
        string message;
        string inAppMessage = $"<a class='text-black-1' href = '{link}'>{timesheet.MissionTitle}</a>";
        if (timesheet.Status == ApprovalStatus.APPROVED)
        {
            message = timesheet.MissionType == MissionTypeEnum.TIME ?
                  $"Volunteer hour entry apprvoed for this mission: {inAppMessage}"
                : $"Volunteer goal entry approved for this mission: {inAppMessage}";
        }
        else
        {
            message = timesheet.MissionType == MissionTypeEnum.GOAL ?
                  $"Volunteer goal entry declined for this mission: {inAppMessage}"
                : $"Volunteer hour entry declined for this mission: {inAppMessage}";
        }
        return message;
    }
    #endregion
}
