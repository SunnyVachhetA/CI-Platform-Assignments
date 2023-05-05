using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Volunteer.Controllers;
[Area("Volunteer")]
[AllowAnonymous]
public class CmsPageController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public CmsPageController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var allCms = await _serviceUnit.CmsPageService.LoadAllActiveCmsPageAsync();

            var result = allCms
                .Select(page => new { id = page.CmsPageId, title = page.Title });

            return Json(result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during load all cms: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Page(short id)
    {
        try
        {
            if (id <= 0) return View("_ErrorView");
            var vm = await _serviceUnit.CmsPageService.LoadCmsPageDetailsAsync(id);
            return View("_CmsPage", vm);
        }
        catch (Exception e)
        {
            Console.WriteLine("error during cms page load: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return View("_ErrorView");
        }
    }
}
