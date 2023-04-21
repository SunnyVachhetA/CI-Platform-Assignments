using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class MissionController : Controller
{
    private readonly IServiceUnit _serviceUnit;

    public MissionController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }

    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            IEnumerable<AdminMissionVM> missions = _serviceUnit.MissionService.LoadAllMissionsAdmin();
            return PartialView("_Missions", missions);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while loading missions [admin]: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult Search(string searchKey)
    {
        try
        {
            IEnumerable<AdminMissionVM> missions = _serviceUnit.MissionService.SearchMission(searchKey);
            return PartialView("_Missions", missions);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while searching missions [admin]: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
