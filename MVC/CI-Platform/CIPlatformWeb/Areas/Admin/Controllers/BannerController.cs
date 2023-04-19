using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class BannerController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public BannerController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
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
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during add banner: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
