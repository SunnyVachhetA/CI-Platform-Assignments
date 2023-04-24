using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class MissionController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public MissionController(IServiceUnit serviceUnit, IWebHostEnvironment webHostEnvironment)
    {
        _serviceUnit = serviceUnit;
        _webHostEnvironment = webHostEnvironment;
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

    [HttpGet]
    public async Task<IActionResult> TimeMission(long? id)
    {
        try
        {
            TimeMissionVM vm = null;
            if (id == 0) vm = new TimeMissionVM();

            vm.CityList = await _serviceUnit.CityService.GetAllCitiesAsync();
            vm.CountryList = await _serviceUnit.CountryService.GetAllCountriesAsync();
            vm.SkillList = await _serviceUnit.SkillService.GetAllSkillsAsync();
            vm.ThemeList = await _serviceUnit.ThemeService.GetAllThemesAsync();
            return PartialView("_AddTimeMission", vm);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while time mission[get]: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> TimeMission(TimeMissionVM mission)
    {
        try
        {
            if (!ModelState.IsValid) return NoContent();
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            await _serviceUnit.MissionService.CreateTimeMission(mission, wwwRootPath);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while time mission[post]: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
