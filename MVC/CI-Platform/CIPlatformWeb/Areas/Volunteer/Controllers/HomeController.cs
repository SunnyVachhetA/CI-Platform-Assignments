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
            return View();
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
    }
}