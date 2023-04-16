﻿using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class UserController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public UserController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
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
}