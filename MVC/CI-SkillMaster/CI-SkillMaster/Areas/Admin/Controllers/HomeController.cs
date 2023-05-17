using Microsoft.AspNetCore.Mvc;

namespace CI_SkillMaster.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
