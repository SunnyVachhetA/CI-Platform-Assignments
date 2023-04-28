using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CIPlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace CIPlatformWeb.Areas.Volunteer.Controllers
{
    [Area("Volunteer")]
    [Authentication]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceUnit _serviceUnit;
        
        public HomeController(ILogger<HomeController> logger, IServiceUnit serviceUnit)
        {
            _logger = logger;
            _serviceUnit = serviceUnit;
        }

        
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
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
        public IActionResult TestAjax(FilterModel filterModel)
        {
            try
            {
                List<MissionCardVM> filteredMissions = _serviceUnit.MissionService.FilterMissions(filterModel);
                var result = _serviceUnit.MissionService.CreateMissionLanding(filteredMissions);
                ViewBag.MissionCount = ( result.missionList == null ) ? 0 : result.missionList?.Count();
                result.missionList = result.missionList?.Skip((filterModel.Page - 1) * 9).Take(9).ToList();
                return PartialView("_MissionIndexListing", result);
            }
            catch (Exception) { return StatusCode(404); }
        }


        //New version of filtering mission: TestAjax()
        [HttpPost]
        public async Task<IActionResult> FilterMissions(FilterModel filterModel)
        {
            try
            {
                var missionList = await _serviceUnit.MissionService.FilterMissionsCard(filterModel);
                ViewBag.MissionCount = missionList.Item2;
                return PartialView("_MissionListing", missionList.Item1);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while filtering mission: " + e.Message);
                Console.WriteLine(e.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpGet]
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

        //New version of LoadMissionsIndexAjax
        [HttpGet]
        public IActionResult LoadMissionCardIndex(int page)
        {
            try
            {
                var missionList = _serviceUnit.MissionService.LoadAllMissionCards(page);
                ViewBag.MissionCount = missionList.Item2;
                return PartialView("_MissionListing", missionList.Item1);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during loading missions card on index: " + e.Message);
                Console.WriteLine(e.StackTrace);
                return StatusCode(500);
            }
        }

        //Async version
        [HttpGet]
        [Route("/Volunteer/Home/LoadMissionCardIndexAsync")]
        public async Task<IActionResult> LoadMissionCardIndexAsync(int page)
        {
            try
            {
                (IEnumerable<MissionVMCard>, long) missionList =
                    await _serviceUnit.MissionService.LoadAllMissionCardsAsync(page);
                ViewBag.MissionCount = missionList.Item2;
                return PartialView("_MissionListing", missionList.Item1);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during loading missions card on index: " + e.Message);
                Console.WriteLine(e.StackTrace);
                return StatusCode(500);
            }
        }
        #endregion 

    }
}


/*
  int[]? countryList, int[]? cityList, string? searchKeyword, int[]? themeList, int[]? skillList, 
            byte? sortBy, int page, long? userId
 */