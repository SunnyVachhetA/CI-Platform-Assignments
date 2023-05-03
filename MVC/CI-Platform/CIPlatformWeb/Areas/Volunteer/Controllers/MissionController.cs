using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Volunteer.Controllers;
[Area("Volunteer")]
[Authentication]
public class MissionController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public MissionController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }

    [AllowAnonymous]
    public IActionResult Index( long id )
    {
        MissionCardVM missionDetails = _serviceUnit.MissionService.LoadMissionDetails( id );
        if (missionDetails is null) return View("_ErrorView");
        //ViewBag.TotalVolunteers = missionDetails.RecentVolunteers.LongCount();
        missionDetails.TotalVolunteers = missionDetails.RecentVolunteers.LongCount();

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
    [AllowAnonymous]
    public async Task<IActionResult> MissionRating(long missionId)
    {
        
            (long volunteerCount, byte avgRating) result = await _serviceUnit.MissionRatingService.GetAverageMissionRating(missionId);
            ViewBag.VolunteerCount = result.volunteerCount;
            ViewBag.AvgRating = result.avgRating;
            return PartialView("_Rating");
        
        
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> RelatedMissionByTheme(long missionId, short themeId)
    {
        try
        {
            var result = await _serviceUnit.MissionService.LoadRelatedMissionBasedOnTheme(themeId, missionId);
        
            return PartialView("_RelatedMissions", result);
        }
        catch(Exception e)
        {
            Console.WriteLine("Error Occured1: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> MissionComments(long missionId, long userId, bool isCommentExists) //Load mission comments
    {
        var result = await _serviceUnit.CommentService.GetAllComments( missionId, userId, isCommentExists );
        return PartialView("_CommentView", result);
    }

    [HttpPost]
    public async Task<IActionResult> MissionComments( long userId, long missionId, string commentText )
    {
        CommentVM comment = new()
        {
            UserId = userId,
            MissionId = missionId,
            Comment = commentText
        };

        await _serviceUnit.CommentService.AddMissionComment( comment );

        var result = await _serviceUnit.CommentService.GetAllComments(missionId, userId, true);
        return PartialView("_CommentView", result);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> RecentVolunteers(long missionId, int page)
    {
        try
        {
            List<RecentVolunteersVM> recentVolunteers = new();
            recentVolunteers = await _serviceUnit.MissionApplicationService.FetchRecentVolunteers(missionId);

            if( recentVolunteers.LongCount() != 0 )
            {
                recentVolunteers = recentVolunteers.Skip((page - 1) * 2).Take(2).ToList();
            }
            return PartialView("_RecentVolunteers", recentVolunteers);
        }
        catch(Exception e)
        {
            Console.WriteLine("Error while fetching recent volunteers: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
