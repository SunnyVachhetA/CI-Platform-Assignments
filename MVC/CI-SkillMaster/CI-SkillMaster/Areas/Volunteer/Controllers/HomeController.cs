using CI_SkillMaster.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CI_SkillMaster.Controllers;

[Area("Volunteer")]
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
    public IActionResult Error()
    {
        _logger.LogInformation("Executing {Action}", nameof(Error));
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
