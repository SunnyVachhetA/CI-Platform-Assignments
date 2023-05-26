using CI_SkillMaster.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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
        var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        var error = new ErrorViewModel
        {
            ErrorCode = HttpContext.Response.StatusCode,
            Message = exception.Error.Message,
            Type = exception.Error.GetType().Name
        };

        return Request.Headers["X-Requested-With"] == "XMLHttpRequest" ? Json(error) : View(error);
    }

}