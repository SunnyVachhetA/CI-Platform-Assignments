﻿using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
[Route("/Volunteer/User/")]
public class UserController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    private readonly IEmailService _emailService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public UserController(IServiceUnit serviceUnit, IEmailService emailService, IWebHostEnvironment webHostEnvironment)
    {
        _serviceUnit = serviceUnit;
        _emailService = emailService;
        _webHostEnvironment = webHostEnvironment;
    }

    [Route("UserProfile", Name = "UserProfile")]
    public IActionResult Index(long id)
    {
        try
        {
            var countryList = _serviceUnit.CountryService.GetAllCountry()?? new List<CountryVM>();
            var cityList = _serviceUnit.CityService.GetAllCities()?? new List<CityVM>();
            var skillList = _serviceUnit.SkillService.GetAllSkills() ?? new List<SkillVM>();
            UserProfileVM userProfile = _serviceUnit.UserService.LoadUserProfile(id) ?? throw new ArgumentNullException("_serviceUnit.UserService.LoadUserProfile(id)");
            
            userProfile.CityList = cityList;
            userProfile.CountryList = countryList;
            userProfile.AllSkills = skillList;
            return View("UserProfile", userProfile);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during loading user profile: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("EditUserProfile", Name = "EditUserProfile")]
    public IActionResult EditUserProfile(UserProfileVM userProfile, List<short> preloadedSkillList, List<short> finalSkillList)
    {
        if (!ModelState.IsValid)
        {
            TempData["edit-profile-fail"] = "Provide valid information!";
            return RedirectToAction("UserProfile", userProfile);
        }

        _serviceUnit.UserService.UpdateUserDetails(userProfile);
        _serviceUnit.UserSkillService.UpdateUserSkills( userProfile.UserId, preloadedSkillList, finalSkillList );
        TempData["edit-profile-success"] = "Your changes have been saved!";
        return RedirectToAction("Index", "Home");
    }

    [HttpPatch]
    [Route("ChangePassword", Name = "ChangePassword")]
    public IActionResult ChangePassword(ChangePasswordVM passwordVm)
    {
        try
        {
            bool isPasswordValid = _serviceUnit.UserService.CheckOldCredentialAndUpdate(passwordVm);

            return isPasswordValid ? Ok() : NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception during change password: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
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
            UserRegistrationVM user = _serviceUnit.UserService.ValidateUserCredential(credential);
 
            if (user != null)
            {
                CreateUserLoginSession(user);
                TempData["login-success"] = "Successfully logged in as " + user.FirstName + " " + user.LastName;
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
        HttpContext.Session.SetString("UserAvatar", user.Avatar);
        HttpContext.Session.SetString("UserEmail", user.Email);
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
            TempData["multiRequestError"] = "Please wait 30 minutes for new reset password link!";
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
            TempData["TokenNotFound"] = "Reset Password link is expired!\nReset password again to change password!";
            return RedirectToAction("ForgotPassword");
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
                _serviceUnit.PasswordResetService.DeleteResetPasswordToken(reset.Email);
                return RedirectToAction("Login");
            }
            catch(Exception)
            {
                return NotFound();
            }
        }
        return View(reset);
    }

    [Route("Registration")]
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
            UserRegistrationVM registeredUser = _serviceUnit.UserService.ValidateUserCredential( new UserLoginVM {  Email = user.Email, Password = user.Password } );
            CreateUserLoginSession(registeredUser);
            TempData["registratoin-success"] = "You have successfully registered.";
            return RedirectToAction("Index", "Home");
        }
        else return View(user);
    }

    [Route("Logout", Name = "Logout")]
    [HttpGet]
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("UserName");
        HttpContext.Session.Remove("UserId");
        HttpContext.Session.Remove("Avatar");
        TempData["logout-success"] = "You have been successfully logged out.";
        return Json(new { redirectToUrl = Url.Action("Index", "Home") });
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("ForgotPasswordV1", Name = "ForgotPasswordPostV1")]
    public IActionResult ForgotPasswordV1(string email)
    {
        if (email == null || !ModelState.IsValid) { return NotFound(); }

        TokenStatus tokenStatus = _serviceUnit.PasswordResetService.GetTokenStatus(email);

        if (tokenStatus == TokenStatus.Empty || tokenStatus == TokenStatus.Expired)
        {
            PasswordResetVM resetVm = GenerateTokenObject(email);
            _serviceUnit.PasswordResetService.AddResetPasswordToken(resetVm);
            TempData["TokenMessage"] = "Reset password link is sent to your email address!";
            return RedirectToAction("Login", "User");
        }
        else
        {
            TempData["multiRequestError"] = "Please check your email for reset password link!(30 Minute Timeout)";
        }

        return RedirectToAction("ForgotPassword");
    }

    #region AJAX CALLS
    [HttpGet]
    [Route("AddMissionToFavourite", Name = "AddFavourite")]
    public async Task<IActionResult> AddMissionToFavourite(long missionId, long userId, bool isFav)
    {
        if (missionId == 0 || userId == 0) return StatusCode(404, "Mission ID or User ID not found!");
        try
        {
            if (!isFav)
            {
                await _serviceUnit.FavouriteMissionService.AddMissionToUserFavourite(userId, missionId);
                return StatusCode(201);
            }
            else
            {
                await _serviceUnit.FavouriteMissionService.RemoveMissionFromUserFavrouite(userId, missionId);
                return NoContent();
            }
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while retrieving data.");
        }
    }
    
    [HttpGet]
    [Route("MissionUsersInvite", Name = "GetMissionUsersInvite")] 
    public IActionResult MissionUsersInvite( long userId, long missionId )
    {
        IEnumerable<UserInviteVm> inviteList = Enumerable.Empty<UserInviteVm>();

        var result = _serviceUnit.UserService.FetchAllUsers(true)?.Where( user => user.UserId != userId )!;

        if(result.Any())
        {
            inviteList = _serviceUnit.MissionInviteService.fetchUserMissionInvites( result, userId, missionId );
        }

        return PartialView("_RecommendMission", inviteList);
    }

    [HttpPost]
    [Route("SendMissionInvites", Name = "SendMissionInvites")]
    public async Task<IActionResult> SendMissionInvites(long userId, long missionId, long[] recommendList)
    {
        try
        {
            var userEmailList = await _serviceUnit.MissionInviteService.SaveMissionInviteFromUser(userId, missionId, recommendList);
            var senderUserName =  await _serviceUnit.UserService.GetUserName( userId );

            string missionInviteLink = Url.Action("Index", "Mission", new { id = missionId }, "https")?? string.Empty;
            _serviceUnit.UserService.SendUserMissionInviteService( userEmailList, senderUserName, missionInviteLink, _emailService);
            return StatusCode(201);
        }
        catch(Exception e)
        {
            Console.WriteLine("Error occured while send mission invites: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    //To update user avatar
    [HttpPatch]
    [Route("UserAvatar", Name = "UserAvatar")]
    public IActionResult UserAvatar(IFormFile file, string avatar, long userId)
    {
        try
        {
            if (file == null) return BadRequest();
            string fileName = file.FileName;
            string prevFileName = avatar.Split("\\")[^1];

            string wwwRootPath = _webHostEnvironment.WebRootPath;

            string directoryPath = $@"{wwwRootPath}\images\user\";
            if (!fileName.Equals(prevFileName))
            {
                StoreMediaService.DeleteFileFromWebRoot( Path.Combine(directoryPath, prevFileName) );
            }

            _serviceUnit.UserService.UpdateUserAvatar( file, wwwRootPath, userId );
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during profile picture upload: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("ContactUs", Name="ContactUs")]
    public IActionResult ContactUs( long userId, string userName, string userEmail )
    {
        ContactUsVM contactUsVm = new()
        {
            UserId = userId,
            UserName = userName,
            Email = userEmail
        };
        return PartialView("_ContactUs", contactUsVm);
    }

    [HttpPost]
    [Route("ContactUsPost", Name = "ContactUsPost")]
    public IActionResult ContactUsPost(long UserId, string Subject, string Message)
    {
        try
        {
            _serviceUnit.ContactUsService.AddContactMessage(UserId, Subject, Message);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during contact us post: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    #endregion
}
