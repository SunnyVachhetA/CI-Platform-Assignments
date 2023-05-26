using CI_SkillMaster.Utility;
using CI_SkillMaster.Utility.Filter;
using CI_SkillMaster.Utility.JwtUtil;
using CISkillMaster.Entities.DTO;
using CISkillMaster.Entities.Enum;
using CISkillMaster.Entities.Exceptions;
using CISkillMaster.Services.Abstract;
using CISkillMaster.Services.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CI_SkillMaster.Areas.Volunteer.Controllers;

[Area("Volunteer")]
//[ServiceFilter(typeof(GlobalExceptionAttribute))]
public class UserController : Controller
{
    private readonly ILoggerAdapter<UserController> _logger;
    private readonly IUserService _userService;
    private readonly IConfiguration _config;
    public UserController(ILoggerAdapter<UserController> logger, IUserService userService, IConfiguration config)
    {
        _logger = logger;
        _userService = userService;
        _config = config;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

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
        JwtTokenUtil jwtUtil = new(_config);
        string token = jwtUtil.GenerateToken(userInformation);
        CreateUserLoginSession(userInformation, token);
        return RedirectToHome(userInformation.Role, credential.Email);
    }

    private void CreateUserLoginSession(UserInformationDTO userInformation, string token)
    {
        HttpContext.Session.SetString("UserName", userInformation.UserName);
        HttpContext.Session.SetString("Role", userInformation.Role.ToString());
        HttpContext.Session.SetString("Email", userInformation.Email);

        var cookieOptions = new CookieOptions()
        {
            Expires = DateTime.UtcNow.AddMinutes(15),
            HttpOnly = true,
        };
        Response.Cookies.Append("Token", token, cookieOptions);
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
        return RedirectToAction("Index", "Home", new { area });
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return Json(new { redirectToUrl = Url.Action("Login", "User", new { area = "Volunteer" }) });
    }

    
}
