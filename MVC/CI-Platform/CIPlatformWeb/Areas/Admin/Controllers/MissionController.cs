
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Areas.Admin.Utilities;
using CIPlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
[AdminAuthentication]
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
    public async Task<IActionResult> TimeMission(long id)
    {
        try
        {
            var vm = id == 0 ? new TimeMissionVM() : await _serviceUnit.MissionService.LoadEditTimeMissionDetails(id);

            vm.CityList = await _serviceUnit.CityService.GetAllCitiesAsync();
            vm.CountryList = await _serviceUnit.CountryService.GetAllCountriesAsync();
            vm.SkillList = (id == 0)
                ? await _serviceUnit.SkillService.GetAllActiveSkillsAsync()
                : await _serviceUnit.SkillService.GetAllSkillsAsync();
            vm.ThemeList = id == 0
                ? await _serviceUnit.ThemeService.GetAllActiveThemesAsync()
                : await _serviceUnit.ThemeService.GetAllThemesAsync();

            return PartialView(id == 0 ? "_AddTimeMission" : "_EditTimeMission", vm);
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
            var id = await _serviceUnit.MissionService.CreateTimeMission(mission, wwwRootPath);
            if (mission.IsActive ?? false)
            {
                string title = mission.Title;
                string link = Url.Action("Index", "Mission", new { area = "Volunteer", id = id }, "https")!;
                var emailList = await FetchEmailSubscriberList(link, title, NotificationTypeEnum.NEW, NotificationTypeMenu.NEW_MISSIONS);
                _ = _serviceUnit.PushNotificationService.PushEmailNotificationToSubscriberAsync(title, link, emailList, NotificationTypeMenu.NEW_MISSIONS);
            }
            return StatusCode(201);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while time mission[post]: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }


    [HttpGet]
    public async Task<IActionResult> GoalMission(long id)
    {
        try
        {
            GoalMissionVM vm = (id == 0)
                ? new GoalMissionVM()
                : await _serviceUnit.MissionService.LoadEditGoalMissionDetails(id);

            vm.CityList = await _serviceUnit.CityService.GetAllCitiesAsync();
            vm.CountryList = await _serviceUnit.CountryService.GetAllCountriesAsync();
            vm.SkillList = (id == 0)
                ? await _serviceUnit.SkillService.GetAllActiveSkillsAsync()
                : await _serviceUnit.SkillService.GetAllSkillsAsync();
            vm.ThemeList = id == 0
                ? await _serviceUnit.ThemeService.GetAllActiveThemesAsync()
                : await _serviceUnit.ThemeService.GetAllThemesAsync();

            return PartialView(id == 0 ? "_AddGoalMission" : "_EditGoalMission", vm);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while time mission[get]: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [ServiceFilter(typeof(AjaxErrorFilter))]
    [HttpPost]
    public async Task<IActionResult> GoalMission(GoalMissionVM mission)
    {

        if (!ModelState.IsValid) return NoContent();
        string wwwRootPath = _webHostEnvironment.WebRootPath;
        var id = await _serviceUnit.MissionService.CreateGoalMission(mission, wwwRootPath);
        if (mission.IsActive ?? false)
        {
            string title = mission.Title;
            string link = Url.Action("Index", "Mission", new { area = "Volunteer", id = id }, "https")!;
            var emailList = await FetchEmailSubscriberList(link, title, NotificationTypeEnum.NEW, NotificationTypeMenu.NEW_MISSIONS);
            _ = _serviceUnit.PushNotificationService.PushEmailNotificationToSubscriberAsync(title, link, emailList, NotificationTypeMenu.NEW_MISSIONS);
        }
        return StatusCode(201);

    }

    [HttpPut]
    public async Task<IActionResult> EditTimeMission(TimeMissionVM mission, IEnumerable<string> preloadedMediaList,
        IEnumerable<string> preloadedDocumentPathList, IEnumerable<short> preloadedSkill)
    {
        try
        {
            if (!ModelState.IsValid) return NoContent();
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            await _serviceUnit.MissionService.UpdateTimeMission(mission, preloadedMediaList, preloadedDocumentPathList, preloadedSkill, wwwRootPath);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while edit time mission[put]: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }


    [HttpPut]
    public async Task<IActionResult> EditGoalMission(GoalMissionVM mission, IEnumerable<string> preloadedMediaList,
        IEnumerable<string> preloadedDocumentPathList, IEnumerable<short> preloadedSkill)
    {
        try
        {
            if (!ModelState.IsValid) return NoContent();
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            await _serviceUnit.MissionService.UpdateGoalMission(mission, preloadedMediaList, preloadedDocumentPathList, preloadedSkill, wwwRootPath);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while edit time mission[put]: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> DeActivate(long id)
    {
        try
        {
            if (id <= 0) return NotFound();

            await _serviceUnit.MissionService.UpdateMissionActiveStatus(id, 0);

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while de-activate mission[put]: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> Restore(long id)
    {
        try
        {
            if (id <= 0) return NotFound();

            await _serviceUnit.MissionService.UpdateMissionActiveStatus(id, 1);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while activate mission[put]: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> Close(long id)
    {
        try
        {
            if (id <= 0) return NotFound();
            await _serviceUnit.MissionService.UpdateMissionCloseStatus(id, 0);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while close mission[put]: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    #region Helper Methods
    private async Task<List<UserContactVM>> FetchEmailSubscriberList(string link, string title, NotificationTypeEnum type, NotificationTypeMenu notificationFor)
    {
        string message = @$"New mission - <a href='{link}' class='text-black-1'>{title}</a>";
        //var emailSubscriptionList = await _serviceUnit.PushNotificationService.PushNotificationToAllUsers(message, type, notificationFor);
        var emailSubscriptionList = await _serviceUnit.PushNotificationService.PushNotificationToAllUsersSPAsync(message, type, notificationFor);
        return emailSubscriptionList;
    }
    #endregion
}
