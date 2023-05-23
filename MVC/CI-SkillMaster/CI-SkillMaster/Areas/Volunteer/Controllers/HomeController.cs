using CI_SkillMaster.Models;
using CI_SkillMaster.Utility.Filter;
using Microsoft.AspNetCore.Mvc;

namespace CI_SkillMaster.Controllers;

[Area("Volunteer")]
[ServiceFilter(typeof(GlobalExceptionAttribute))]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Executing {Action}", nameof(Index));
        return View();
    }

    public IActionResult Privacy()
    {
        _logger.LogInformation("Executing {Action}", nameof(Privacy));
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(ErrorViewModel model)
    {
        _logger.LogInformation("Executing {Action}", nameof(Error));
        return View(model);
    }
}
