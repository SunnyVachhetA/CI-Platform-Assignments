using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace CIPlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
[AdminAuthentication]
public class UserController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public UserController(IServiceUnit serviceUnit, IWebHostEnvironment webHostEnvironment)
    {
        _serviceUnit = serviceUnit;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public IActionResult Users()
    {
        var allUsers = _serviceUnit.UserService.GetSortedUserList();
        return PartialView("_Users", allUsers);
    }

    [HttpPatch]
    public IActionResult DeleteUser(long userId)
    {
        try
        {
            int updateResult = _serviceUnit.UserService.UpdateUserStatus(userId, 0);
            if (updateResult == 0) return StatusCode(204);

            var allUsers = _serviceUnit.UserService.GetSortedUserList();
            return PartialView("_Users", allUsers);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception during delete user: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public IActionResult RestoreUser(long userId)
    {
        try
        {
            int updateResult = _serviceUnit.UserService.UpdateUserStatus(userId, 1);
            if (updateResult == 0) return StatusCode(204);

            var allUsers = _serviceUnit.UserService.GetSortedUserList();
            return PartialView("_Users", allUsers);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception during restore user: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult SearchUser(string? searchKey)
    {
        
        try
        {
            IEnumerable<UserRegistrationVM> filterUsers = _serviceUnit.UserService.FilterUserBySearchKey(searchKey!);
            return PartialView("_Users", filterUsers);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while search user" + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult Add()
    {
        try
        {
            AdminUserInfoVM user = new()
            {
                CityList = _serviceUnit.CityService.GetAllCities(),
                CountryList = _serviceUnit.CountryService.GetAllCountry()
            };
            return PartialView("_AddUser", user);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during add user [get]: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(AdminUserInfoVM user)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string token = Guid.NewGuid().ToString();
            var href = Url.Action("Login", "User", new { area="Volunteer", _email = user.Email.Trim().ToLower(), _token = token }, "https");
            var userId = await _serviceUnit.UserService.AddUserByAdmin(user, wwwRootPath, href, token);
            _serviceUnit.NotificationSettingService.CreateUserSetting(userId);
            _serviceUnit.UserNotificationCheckService.CreateUserLastCheck(userId);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during Add user[post]: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public bool CheckIsEmailUnique(string email) => _serviceUnit.UserService.CheckIsEmailUnique(email.Trim());

    [HttpGet]
    public IActionResult Edit(long id)
    {
        try
        {
            AdminUserInfoVM user = _serviceUnit.UserService.LoadUserProfileEdit(id);
            user.CityList = _serviceUnit.CityService.GetAllCities();
            user.CountryList = _serviceUnit.CountryService.GetAllCountry();
            return PartialView("_EditUser", user);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during user GET edit: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }


    [HttpPut]
    public async Task<IActionResult> Edit(AdminUserInfoVM user)
    {
        try
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            await _serviceUnit.UserService.UpdateUserByAdmin(user, wwwRootPath);
            return Ok(200);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during user POST edit: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
