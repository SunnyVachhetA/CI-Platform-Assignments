using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class MissionThemeController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public MissionThemeController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }
    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            IEnumerable<ThemeVM> themeList = _serviceUnit.ThemeService.GetAllThemes();
            return PartialView("_themes", themeList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while loading theme list: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult SearchTheme(string? searchKey)
    {
        try
        {
            var themeList = _serviceUnit.ThemeService.SearchThemeByKey(searchKey);
            return PartialView("_themes", themeList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during search cms: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult AddTheme()
    {
        try
        {
            return PartialView("_AddTheme");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while loading Add theme modal: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return NotFound();
        }
    }

    [HttpGet]
    public bool CheckIsThemeUnique(string themeName, short themeId)
    {
        return _serviceUnit.ThemeService.CheckThemeIsUnique(themeName, themeId);
    }

    [HttpPost]
    public IActionResult AddTheme(ThemeVM themeVm)
    {
        try
        {
            _serviceUnit.ThemeService.AddTheme(themeVm);
            var themeList = _serviceUnit.ThemeService.GetAllThemes();
            return PartialView("_themes", themeList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while adding theme: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpDelete]
    public IActionResult DeleteTheme(short themeId)
    {
        try
        {
            bool result = _serviceUnit.MissionService.IsThemeUsedInMission(themeId);
            if (result) return StatusCode(204);

            _serviceUnit.ThemeService.DeleteThemeById(themeId);

            var themeList = _serviceUnit.ThemeService.GetAllThemes();
            return PartialView("_themes", themeList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during delete theme: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public IActionResult DeactivateTheme(short themeId)
    {
        try
        {
            //bool result = _serviceUnit.MissionService.IsThemeUsedInMission(themeId);
            //if (result) return StatusCode(204);

            _serviceUnit.ThemeService.UpdateThemeStatus(themeId);

            var themeList = _serviceUnit.ThemeService.GetAllThemes();
            return PartialView("_themes", themeList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during delete theme: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult Edit(short themeId)
    {
        try
        {
            ThemeVM theme = _serviceUnit.ThemeService.GetThemeDetails(themeId);

            if (theme == null) throw new Exception("No such theme exists with themeId!");

            return PartialView("_EditTheme", theme);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while edit theme GET: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPut]
    public IActionResult Edit(ThemeVM themeVm)
    {
        try
        {
            _serviceUnit.ThemeService.EditTheme(themeVm);

            var themeList = _serviceUnit.ThemeService.GetAllThemes();
            return PartialView("_themes", themeList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while edit theme POST: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public IActionResult Restore(short themeId)
    {
        try
        {
            _serviceUnit.ThemeService.UpdateThemeStatus(themeId, 1);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while theme restore: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
