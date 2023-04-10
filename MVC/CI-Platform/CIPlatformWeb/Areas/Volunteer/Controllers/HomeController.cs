using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace CIPlatformWeb.Areas.User.Controllers
{
    [Area("Volunteer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceUnit _serviceUnit;
        
        public HomeController(ILogger<HomeController> logger, IServiceUnit serviceUnit)
        {
            _logger = logger;
            _serviceUnit = serviceUnit;
        }
        public IActionResult Index()
        {
            var missionLanding = _serviceUnit.MissionService.CreateMissionLanding();
            return View( missionLanding );
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Ajax Call
        [HttpPost]
        public IActionResult TestAjax(int[]? countryList, int[]? cityList, string? searchKeyword, int[]? themeList, int[]? skillList, 
            byte? sortBy, int page, long? userId)
        {
            FilterModel filterModel = new()
            {
                CountryList = countryList,
                CityList = cityList,    
                SearchKeyword = searchKeyword,  
                ThemeList = themeList,
                SkillList = skillList,
                SortBy = (SortByMenu)sortBy,
                UserId = userId
            };
            try
            {
                List<MissionCardVM> filteredMissions = _serviceUnit.MissionService.FilterMissions(filterModel);
                var result = _serviceUnit.MissionService.CreateMissionLanding(filteredMissions);
                ViewBag.MissionCount = ( result.missionList == null ) ? 0 : result.missionList?.Count();
                result.missionList = result.missionList?.Skip((page - 1) * 9).Take(9).ToList();
                return PartialView("_MissionIndexListing", result);
            }
            catch (Exception) { return StatusCode(404); }
        }

        public IActionResult LoadMissionsIndexAjax(int page)
        {
            MissionLandingVM missionLanding = new()
            {
                missionList = new()
            };
            var missionList = _serviceUnit.MissionService.GetAllMissionCards();
            missionLanding.missionList = missionList;
            ViewBag.MissionCount = missionLanding.missionList.Count();
            missionLanding.missionList = missionLanding.missionList.Skip((page - 1) * 9).Take(9).ToList();
            return PartialView("_MissionIndexListing", missionLanding);
        }

        #endregion 

    }
}