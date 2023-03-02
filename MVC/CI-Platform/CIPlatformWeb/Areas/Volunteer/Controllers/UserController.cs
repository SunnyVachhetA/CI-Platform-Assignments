using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
[Route("/Volunteer/User/")]
public class UserController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    private readonly IEmailService _emailService;
    public UserController(IServiceUnit serviceUnit, IEmailService emailService)
    {
        _serviceUnit = serviceUnit;
        _emailService = emailService;
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
    [Route("ForgotPassword", Name = "ForgotPasswordPost")]
    public IActionResult ForgotPassword(string email)
    {
        if (email == null)
        {
            ModelState.AddModelError("EmailError", "Please enter valid email address!");
            return View();
        }
        bool tokenExists = _serviceUnit.PasswordResetService.IsTokenExists(email);
        if( tokenExists )
        {
            ModelState.AddModelError("multiRequestError", "Please check your email box for reset password link!");
            TempData["multiRequestError"] = "Please check your email box for reset password link!";
            return RedirectToAction("ForgotPassword");
        }
        bool result = _serviceUnit.UserService.IsEmailExists(email);
        if (result)
        {
            try
            {
                PasswordResetVM obj = GenerateTokenObject(email);
                _serviceUnit.PasswordResetService.AddResetPasswordToken(obj);
                TempData["TokenMessage"] = "Reset password link is sent to your email address!";
                return RedirectToAction("Login", "User");
            }
            catch(Exception)
            {
                return StatusCode(404); //Not found
            }
        }
        else 
        {
            ModelState.AddModelError("EmailError", "Given email address account does not exsits!!");
        }
        return RedirectToAction("ForgotPassword");
    }

    [Route("ResetPassword", Name = "ResetPassword")]
    public IActionResult ResetPassword(string _email, string _token)
    {

        bool result = _serviceUnit.PasswordResetService.IsTokenExists(_email);

        if (!result)
        {
            /*ModelState.AddModelError("EmailExistsError", "Please check your mailbox for rest password link!");
            return RedirectToAction("ForgotPassword");*/
            return NotFound();
        }

        ResetPasswordPostVM postVm = new()
        {
            Email = _email,
            Token = _token
        };
        return View(postVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("ResetPassword", Name = "ResetPasswordPost")]
    public IActionResult ResetPassword(ResetPasswordPostVM reset)
    {
        if (ModelState.IsValid)
        {
            try
            {
                UserRegistrationVM user = _serviceUnit.UserService.UpdateUserPassword(reset.Email, reset.Password);
                CreateUserLoginSession(user);
                _serviceUnit.PasswordResetService.DeleteResetPasswordToken(reset.Email);
                return RedirectToAction("Index", "Home");
            }
            catch(Exception)
            {
                return NotFound();
            }
        }
        return View(reset);
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

    public PasswordResetVM GenerateTokenObject(string email)
    {
        string token = Guid.NewGuid().ToString();
        var href = Url.Action("ResetPassword", "User", new { _email = email, _token = token }, "https");
        _emailService.SendResetPasswordLink(email, href);
        PasswordResetVM obj = new()
        {
            Email = email,
            Token = token
        };
        return obj;
    }
       
}
