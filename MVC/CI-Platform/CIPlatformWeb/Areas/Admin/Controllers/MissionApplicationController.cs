using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class MissionApplicationController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public MissionApplicationController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }

    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            IEnumerable<MissionApplicationVM> applications = _serviceUnit.MissionApplicationService.LoadApplications();
            return PartialView("_Applications", applications);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during mission application loading: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public IActionResult ApplicationApproval(long id, byte status)
    {
        try
        {
            if (id == 0 || status == 0) return BadRequest();
            _serviceUnit.MissionApplicationService.UpdateApplicationStatus(id, status);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during mission application approval: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult Search(string searchKey)
    {
        try
        {
            IEnumerable<MissionApplicationVM> applications = _serviceUnit.MissionApplicationService.SearchApplication(searchKey);
            return PartialView("_Applications", applications);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during mission application search: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
