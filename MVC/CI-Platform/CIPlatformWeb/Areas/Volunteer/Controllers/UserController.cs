using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;
namespace CIPlatformWeb.Areas.User.Controllers;

[Area("Volunteer")]
[Route("/Volunteer/User/")]
public class UserController : Controller
{
    private readonly IServiceUnit _serviceUnit;

    public UserController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }

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

    [Route("Registration")]
    [Route("/")]
    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("Registration")]
    public IActionResult Registration(UserVM user)
    {
        string email = user.Email;
        if( _serviceUnit.UserService.IsEmailExists(email) )
        {
            ModelState.AddModelError("Email", "Given Email ID " + email + " Is Already Registered!");
        }
        if(ModelState.IsValid)
        {
            Console.Write("Inside Registration");
            _serviceUnit.UserService.Add(user);
            return View("Login");
        }
        else return RedirectToAction("Registration");
    }
}
