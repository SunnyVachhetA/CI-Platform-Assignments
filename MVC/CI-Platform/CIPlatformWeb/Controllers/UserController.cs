using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Controllers;
public class UserController : Controller
{
    [Route("user/login")]
    [Route("user/")]
    public IActionResult Index()
    {
        return View("Login");
    }

    public IActionResult ForgotPassword()
    {
        Console.WriteLine("Forgot Password called");
        return View();
    }

    public IActionResult ResetPassword()
    {
        return View();
    }

    public IActionResult Registration()
    {
        return View();
    }
}
