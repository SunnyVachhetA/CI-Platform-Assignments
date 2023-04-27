using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;

[Area("Admin")]
[AdminAuthentication]
public class MissionApplicationController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public MissionApplicationController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }

    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            IEnumerable<MissionApplicationVM> applications = _serviceUnit.MissionApplicationService.LoadApplications();
            return PartialView("_Applications", applications);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during mission application loading: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public IActionResult ApplicationApproval(long id, byte status)
    {
        try
        {
            if (id == 0 || status == 0) return BadRequest();
            _serviceUnit.MissionApplicationService.UpdateApplicationStatus(id, status);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during mission application approval: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult Search(string searchKey)
    {
        try
        {
            IEnumerable<MissionApplicationVM> applications = _serviceUnit.MissionApplicationService.SearchApplication(searchKey);
            return PartialView("_Applications", applications);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during mission application search: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult ViewUser(long userId)
    {
        try
        {
            UserProfileVM vm = _serviceUnit.UserService.LoadUserProfile(userId)?? throw new Exception("Cannot find user : " + userId);

            var countryList = _serviceUnit.CountryService.GetAllCountry() ?? new List<CountryVM>();
            var cityList = _serviceUnit.CityService.GetAllCities() ?? new List<CityVM>();
            var skillList = _serviceUnit.SkillService.GetAllSkills() ?? new List<SkillVM>();

            vm.CountryList = countryList;
            vm.CityList = cityList;
            vm.AllSkills = skillList;
            return PartialView("_UserProfile", vm);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while loading user details: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
