using CI_SkillMaster.Areas.Admin.Controllers;
using CI_SkillMaster.Utility;
using CISkillMaster.Entities.DTO;
using CISkillMaster.Entities.Enum;
using CISkillMaster.Services.Abstract;
using CISkillMaster.Services.Logging;
using Microsoft.AspNetCore.Mvc;

namespace CI_SkillMaster.Areas.Volunteer.Controllers;

[Area("Volunteer")]
public class UserController : Controller
{
    private readonly ILoggerAdapter<UserController> _logger;
    private readonly IUserService _userService;
    public UserController(ILoggerAdapter<UserController> logger, IUserService userService)
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

        if (!ModelState.IsValid) return HandleInvalidModelState(credential);

        UserInformationDTO? userInformation = await _userService.SignInUser(credential);

        if (userInformation is null) return HandleInvalidUser(credential, "Invalid Email or Password.");
        
        if (userInformation.Status != Status.Active) 
            return HandleInvalidUser(credential, "Given user is either inactive or deleted. Please contact the admin.");

        _logger.LogInformation("Login success with param {Param}", credential.Email);

        return RedirectToHome(userInformation.Role, credential.Email);
    }

    private IActionResult HandleInvalidModelState(UserLoginDTO credential)
    {
        _logger.LogWarning("Improper format data from user in {Action}", nameof(Login));
        return View(credential);
    }

    private IActionResult HandleInvalidUser(UserLoginDTO credential, string errorMessage)
    {
        _logger.LogWarning("User {Param} is either inactive or deleted", credential.Email);
        ModelState.AddModelError(string.Empty, errorMessage);
        return View(credential);
    }

    private IActionResult RedirectToHome(UserRole role, string email)
    {
        var area = role == UserRole.User ? nameof(Volunteer) : nameof(Admin);
        _logger.LogInformation("Redirecting {Role} {Param} To Home From {Action}", role, email, nameof(Login));
        return RedirectToAction(nameof(Index), nameof(HomeController), new { area });
    }
}
