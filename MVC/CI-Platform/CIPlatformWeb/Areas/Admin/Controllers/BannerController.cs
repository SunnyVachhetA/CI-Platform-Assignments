using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class BannerController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public BannerController(IServiceUnit serviceUnit, IWebHostEnvironment webHostEnvironment)
    {
        _serviceUnit = serviceUnit;
        _webHostEnvironment = webHostEnvironment;
    }
    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<BannerVM> bannerList = _serviceUnit.BannerService.LoadAllBanners();
        return PartialView("_Banners", bannerList);
    }

    [HttpGet]
    public IActionResult Add() => PartialView("_AddBanner");

    [HttpPost]
    public IActionResult Add(BannerVM banner)
    {
        try
        {
            if (!ModelState.IsValid) return NoContent();

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            _serviceUnit.BannerService.Add(banner, wwwRootPath);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during add banner: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult Edit(long bannerId) =>
        PartialView("_EditBanner", _serviceUnit.BannerService.LoadBannerDetails(bannerId));

    [HttpPut]
    public IActionResult Edit(BannerVM banner)
    {
        try
        {
            if(!ModelState.IsValid) return BadRequest();
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            
            _serviceUnit.BannerService.UpdateBanner(banner, wwwRootPath);
            return StatusCode(200);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during edit banner: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }


    [HttpPatch]
    public IActionResult DeActivate(long bannerId)
    {
        try
        {
            _serviceUnit.BannerService.UpdateBannerStatus(bannerId);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during de-activating banner: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public IActionResult Restore(long bannerId)
    {
        try
        {
            _serviceUnit.BannerService.UpdateBannerStatus(bannerId, 1);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during activating banner: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
