using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public HomeController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }
    public IActionResult Index()
    {
        return View();
    }

}
