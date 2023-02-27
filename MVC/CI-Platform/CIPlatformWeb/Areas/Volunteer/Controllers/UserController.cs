using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.User.Controllers;

[Area("Volunteer")]
[Route("/Volunteer/User/")]
public class UserController : Controller
{
    [Route("Login", Name = "Login")]
    public IActionResult Login()
    {
        return View("Login");
    }

    [Route("Forgot-Password", Name = "ForgotPassword")]
    public IActionResult ForgotPassword()
    {
        Console.WriteLine("Forgot Password called");
        return View();
    }

    [Route("Reset-Password", Name = "ResetPassword")]
    public IActionResult ResetPassword()
    {
        return View();
    }

    [Route("Registration", Name = "Registration")]
    public IActionResult Registration()
    {
        return View();
    }
}
