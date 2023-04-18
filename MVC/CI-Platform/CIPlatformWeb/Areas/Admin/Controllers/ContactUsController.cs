using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;
public class ContactUsController : Controller
{
    public IActionResult Index()
    {
        return PartialView("_ContactInquires");
    }
}
