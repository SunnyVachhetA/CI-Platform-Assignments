using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
[Authentication]
public class StoryInviteController : Controller
{
    private readonly IServiceUnit _serviceUnit;

    public StoryInviteController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }
    
}
