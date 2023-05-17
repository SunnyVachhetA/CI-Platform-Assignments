using CI_SkillMaster.Areas.Admin.Controllers;
using CI_SkillMaster.Utility;
using CISkillMaster.Entities.DTO;
using CISkillMaster.Entities.Enum;
using CISkillMaster.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CI_SkillMaster.Areas.Volunteer.Controllers;

[Area("Volunteer")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService; 
    }

    [HttpGet]
    public IActionResult Login() => View(); 

    [HttpPost]
    public async Task<IActionResult> Login([ModelBinder(BinderType = typeof(CleanDataModelBinder))] UserLoginDTO credential)
    {
        _logger.LogInformation("Executing {Action} with Parameters {Param}", nameof(Login), credential.Email);
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Improper format data from user in {Action}", nameof(Login));
            return View(credential);
        }

        UserInformationDTO? userInformation = await _userService.SignInUser(credential);
        
        if(userInformation is null)
        {
            ModelState.AddModelError("", "Invalid Email or Password.");
            return View(credential);
        }

        if (userInformation.Status != UserStatus.Active)
        {
            _logger.LogWarning("User {Param} is either in-active or deleted", credential.Email);
            ModelState.AddModelError("", "Given user either in-active or delted. Please contact admin.");
            return View(credential);
        }
        _logger.LogInformation("Login success with param {Param}", credential.Email);

        var area = userInformation.Role == UserRole.User ? nameof(Volunteer) : nameof(Admin);
        _logger.LogInformation("Redirecting {Role} {Param} To Home From {Action}", userInformation.Role, credential.Email, nameof(Login));
        return RedirectToAction(nameof(Index), nameof(HomeController), new { area });
    }
}
