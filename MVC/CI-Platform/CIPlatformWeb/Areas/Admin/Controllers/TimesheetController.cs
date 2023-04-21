using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class TimesheetController : Controller
{
    private readonly IServiceUnit _serviceUnit;

    public TimesheetController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }

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
    public IActionResult Approve(long timesheetId)
    {
        try
        {
            _serviceUnit.TimesheetService.UpdateTimesheetStatus(timesheetId, 1);
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
    public IActionResult Decline(long timesheetId)
    {
        try
        {
            _serviceUnit.TimesheetService.UpdateTimesheetStatus(timesheetId, 2);
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
}
