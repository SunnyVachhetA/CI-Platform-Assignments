using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
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

    #region Methods
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
    public async Task<IActionResult> ApplicationApproval(long id, byte status)
    {
        try
        {
            if (id == 0 || status == 0) return BadRequest();
            await _serviceUnit.MissionApplicationService.UpdateApplicationStatus(id, status);

            await PushMissionApplicationNotification(id);
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

    #endregion

    #region Helper methods
    private async Task PushMissionApplicationNotification(long id)
    {
        MissionApplicationVM application = await _serviceUnit.MissionApplicationService.FetchUserMissionApplicationAsync(id);

        UserNotificationTemplate template = UserNotificationTemplate.ConvertFromMissionApplication(application);

        string link = Url.Action("Index", "Mission", new { area = "Volunteer", id = application.MissionId }, "https")!;
        string inAppLink = $"<a href='{link}' class='text-black-1'>{application.MissionTitle}</a>";

        template.Message = application.ApprovalStatus == ApprovalStatus.APPROVED ?
                  $"Volunteering request has been approved for this mission: {inAppLink}"
                  : $"Volunteering request has been declined for this mission: {inAppLink}";

        var isOpenForEmail = await _serviceUnit.PushNotificationService.PushNotificationToUserAsync(template);

        if (isOpenForEmail)
            _ = _serviceUnit.PushNotificationService.PushEmailNotificationToUserAsync(template, link);
        
    }
    #endregion
}
