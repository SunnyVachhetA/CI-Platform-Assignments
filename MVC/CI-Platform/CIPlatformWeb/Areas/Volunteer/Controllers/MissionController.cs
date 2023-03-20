﻿using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;
namespace CIPlatformWeb.Areas.Volunteer.Controllers;
[Area("Volunteer")]
public class MissionController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public MissionController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }
    
    public IActionResult Index( long id )
    {
        MissionCardVM missionDetails = _serviceUnit.MissionService.LoadMissionDetails( id );
        return View( missionDetails );
    }

    [HttpPost]
    public async Task<IActionResult> MissionRating( long missionId, long userId, byte rating )
    {
        try
        {
            await _serviceUnit.MissionRatingService.SaveUserMissionRating(missionId, userId, rating);
            (long volunteerCount, byte avgRating) result = await _serviceUnit.MissionRatingService.GetAverageMissionRating(missionId);
            await _serviceUnit.MissionService.UpdateMissionRating( missionId, result.avgRating );
            ViewBag.VolunteerCount = result.volunteerCount;
            ViewBag.AvgRating = result.avgRating;
            Console.WriteLine( "average rating and Count : { " + result.avgRating + ", " + result.volunteerCount + " }.");
            return PartialView("_Rating");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error Occured2: " + e.Message);
            Console.WriteLine( e.StackTrace );
            return StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMissionRating(long missionId, long userId, byte rating)
    {
        try
        {
            MissionRatingVM ratingVm = new()
            {
                MissionId = missionId,
                UserId = userId,
                Rating = rating,
                UpdatedAt = DateTimeOffset.Now
            };
            await _serviceUnit.MissionRatingService.UpdateUserMissionRating(ratingVm);
            (long volunteerCount, byte avgRating) result = await _serviceUnit.MissionRatingService.GetAverageMissionRating(missionId);
            await _serviceUnit.MissionService.UpdateMissionRating(missionId, result.avgRating);
            ViewBag.VolunteerCount = result.volunteerCount;
            ViewBag.AvgRating = result.avgRating;
            Console.WriteLine("average rating and Count : { " + result.avgRating + ", " + result.volunteerCount + " }.");
            return PartialView("_Rating");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error Occured3: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public async Task<IActionResult> MissionRating(long missionId)
    {
        
            (long volunteerCount, byte avgRating) result = await _serviceUnit.MissionRatingService.GetAverageMissionRating(missionId);
            ViewBag.VolunteerCount = result.volunteerCount;
            ViewBag.AvgRating = result.avgRating;
            return PartialView("_Rating");
        
        
    }

    [HttpGet]
    public IActionResult RelatedMissionByTheme(long missionId, short themeId)
    {
        try
        {
            var result = _serviceUnit.MissionService.LoadRelatedMissionBasedOnTheme(themeId, missionId).ToList();
        
            result = result.Take(3).ToList();
            return PartialView("_RelatedMissions", result);
        }
        catch(Exception e)
        {
            Console.WriteLine("Error Occured1: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500, e.Message);
        }
    }

}
