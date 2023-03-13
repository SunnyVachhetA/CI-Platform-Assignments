using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Diagnostics;

namespace CIPlatformWeb.Areas.User.Controllers
{
    [Area("Volunteer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceUnit _serviceUnit;
        private List<CountryVM> countryList = new();
        private List<CityVM> cityList = new();
        private List<ThemeVM> themeList = new();
        private List<SkillVM> skillList = new();
        public List<MissionCardVM> missionList = new();
        private MissionLandingVM missionLanding = new()
        {
             countryList = new(),
             cityList = new(),
             themeList = new(),
             missionList = new(),
             skillList = new()
        };
        public HomeController(ILogger<HomeController> logger, IServiceUnit serviceUnit)
        {
            Console.WriteLine( "Controller Called...." );
            _logger = logger;
            _serviceUnit = serviceUnit;
            countryList = _serviceUnit.CountryService.GetAllCountry();
            cityList = _serviceUnit.CityService.GetAllCities();
            themeList = _serviceUnit.ThemeService.GetAllThemes();
            skillList = _serviceUnit.SkillService.GetAllSkills();
            missionList = _serviceUnit.MissionService.GetAllMissionCards();
            missionLanding = new()
            {
                countryList = countryList,
                cityList = cityList,
                themeList = themeList,
                skillList = skillList,
                missionList = missionList
            };
        }
        public IActionResult Index()
        {
            return View( missionLanding );
        }

        public IActionResult Privacy()
        {
            Console.WriteLine(  );
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Ajax Call
        [HttpPost]
        public IActionResult TestAjax(byte[]? countryList, int[]? cityList, string? searchKeyword, short[]? themeList, short[]? skillList, byte? sortBy)
        {

            FilterModel filterModel = new()
            {
                CountryList = countryList,
                CityList = cityList,    
                SearchKeyword = searchKeyword,  
                ThemeList = themeList,
                SkillList = skillList,
                SortBy = sortBy
            };

            return Ok();
        }
        #endregion 

    }
}