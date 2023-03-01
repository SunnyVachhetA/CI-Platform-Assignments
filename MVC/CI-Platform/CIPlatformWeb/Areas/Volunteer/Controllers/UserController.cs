using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Volunteer.Controllers;

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

    [HttpPost]
    [Route("Login")]
    [ValidateAntiForgeryToken]
    public IActionResult Login(UserLoginVM credential)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine("U Password >>>>>>>>>> " + credential.Password);
            UserRegistrationVM user = _serviceUnit.UserService.ValidateUserCredential(credential);
    
            if (user != null)
            {
                CreateUserLoginSession(user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("PasswordError", "Invalid Email ID or Password!");
            }
        }
        return View();
    }

    public void CreateUserLoginSession(UserRegistrationVM user)
    {
        HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);
        HttpContext.Session.SetString("UserId", user.UserId.ToString()!);
        HttpContext.Session.SetString("UserAvatar", "~/assets/");
    }

    [Route("ForgotPassword")]
    public IActionResult ForgotPassword()   
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ForgotPassword(string email)
    {
        if (email == null)
        {
            ModelState.AddModelError("EmailError", "Please enter valid email address!");
            return View();
        }
        bool result = _serviceUnit.UserService.IsEmailExists(email);
        if (result)
        {

        }
        else 
        {
            ModelState.AddModelError("EmailError", "Given email address account does not exsits!!");
        }
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
    public IActionResult Registration(UserRegistrationVM user)
    {
        string email = user.Email;
        if (_serviceUnit.UserService.IsEmailExists(email))
        {
            ModelState.AddModelError("Email", "Given Email ID is Already Registered!");
        }
        if (ModelState.IsValid)
        {
            _serviceUnit.UserService.Add(user);
            CreateUserLoginSession(user);
            return RedirectToAction("Index", "Home");
        }
        else return View(user);
    }

    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("UserName");
        HttpContext.Session.Remove("UserId");
        HttpContext.Session.Remove("Avatar");
        return RedirectToAction("Login");
    }
}
