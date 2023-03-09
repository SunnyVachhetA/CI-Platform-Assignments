using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Models;
using Microsoft.AspNetCore.Mvc;
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
        //public List<MissionCardVM> missionList = new();
        public List<MissionCardVM> missionList { get; set; } = new();
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
            _logger = logger;
            _serviceUnit = serviceUnit; 
        }
        public IActionResult Index()
        {
            countryList = _serviceUnit.CountryService.GetAllCountry();
            cityList = _serviceUnit.CityService.GetAllCities();
            themeList = _serviceUnit.ThemeService.GetAllThemes();
            skillList = _serviceUnit.SkillService.GetAllSkills();
            missionList = _serviceUnit.MissionService.GetAllMissionCards();
            Console.WriteLine($"FirstMethod - Controller instance hash code: {GetHashCode()}");

            missionLanding = new()
            {
                countryList = countryList,
                cityList = cityList,
                themeList = themeList,
                skillList = skillList,
                missionList = missionList
            };
            return View( missionLanding );
        }

        public IActionResult Privacy()
        {
            Console.WriteLine($"SecondMethod - Controller instance hash code: {GetHashCode()}");
            Console.WriteLine( missionList.Count );
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}