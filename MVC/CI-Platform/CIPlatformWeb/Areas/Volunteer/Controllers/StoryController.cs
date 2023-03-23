using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
public class StoryController : Controller
{
    public IActionResult Index()
    {
        return View("StoryListing");
    }

    public IActionResult ShareStory()
    {
        return View();
    }
}
