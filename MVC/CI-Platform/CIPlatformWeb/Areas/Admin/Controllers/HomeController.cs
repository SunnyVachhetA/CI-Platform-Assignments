using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;

[Area("Admin")]
[AdminAuthentication]
public class HomeController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public HomeController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("UserName");
        HttpContext.Session.Remove("UserId");
        HttpContext.Session.Remove("Avatar");
        HttpContext.Session.Remove("IsAdmin");
        TempData["logout-success"] = "You have been successfully logged out.";
        return Json(new { redirectToUrl = Url.Action("Index", "Home", new { area = "Volunteer" }) });
    }
}
