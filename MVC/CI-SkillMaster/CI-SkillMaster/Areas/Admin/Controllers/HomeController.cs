using CI_SkillMaster.Authentication;
using CI_SkillMaster.Utility.Filter;
using Microsoft.AspNetCore.Mvc;

namespace CI_SkillMaster.Areas.Admin.Controllers;

[Area("Admin")]
[AdminAuthentication]
//[ServiceFilter(typeof(GlobalExceptionAttribute))]
public class HomeController : Controller
{
    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Index()
    {
        return View();
    }
}
