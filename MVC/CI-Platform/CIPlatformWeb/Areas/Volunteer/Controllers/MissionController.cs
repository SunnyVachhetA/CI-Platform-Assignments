using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;
namespace CIPlatformWeb.Areas.Volunteer.Controllers;
[Area("Volunteer")]
public class MissionController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public MissionController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }
    
    public IActionResult Index( long id )
    {
        MissionCardVM missionDetails = _serviceUnit.MissionService.LoadMissionDetails( id );
        var result = _serviceUnit.MissionService.LoadRelatedMissionBasedOnTheme(2);
        return View( missionDetails );
    }

}
