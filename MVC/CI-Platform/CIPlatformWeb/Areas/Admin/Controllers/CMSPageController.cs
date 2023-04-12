using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class CMSPageController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public CMSPageController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }
    public IActionResult Index()
    {
        IEnumerable<CMSPageVM> cmsPages = _serviceUnit.CmsPageService.LoadAllCmsPages();
        return PartialView("_CMSPages", cmsPages);
    }

    [HttpGet]
    public IActionResult AddCMS()
    {
        return PartialView("_AddCMS");
    }

    [HttpPost]
    public IActionResult AddCMS( CMSPageVM cmsPage )
    {
        try
        {
            _serviceUnit.CmsPageService.AddCMSPage(cmsPage);

            var cmsPages = _serviceUnit.CmsPageService.LoadAllCmsPages();
            return PartialView("_CMSPages", cmsPages);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during add cms[post]: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public bool IsSlugUnique(string slug, short id)
        => 
        id == 0 ? _serviceUnit.CmsPageService.CheckIsSlugUnique(slug) : _serviceUnit.CmsPageService.CheckIsSlugUnique(slug, id);

    [HttpGet]
    public IActionResult SearchCMS(string searchKey)
    {
        try
        {
            var cmsPages = _serviceUnit.CmsPageService.SearchCMSPageByKey(searchKey);
            return PartialView("_CMSPages", cmsPages);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during search cms: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
