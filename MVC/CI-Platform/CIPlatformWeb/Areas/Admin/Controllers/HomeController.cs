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

    [HttpGet]
    public IActionResult Users()
    {
        IEnumerable<UserRegistrationVM> allUsers = _serviceUnit.UserService.FetchAllUsers( isActiveFlag:false );
        allUsers = allUsers.OrderBy( user => user.Status );
        return PartialView("_Users", allUsers);
    }
}
