namespace CIPlatform.Services.Service.Interface;
public interface IServiceUnit
{
    IUserService UserService { get; }
    IPasswordResetService PasswordResetService { get; }
    ICountryService CountryService { get; }    
    ICityService CityService { get; }
    ISkillService SkillService { get; }
    IThemeService ThemeService { get; }
    IMissionApplicationService MissionApplicationService { get; }
    IFavouriteMissionService  FavouriteMissionService { get; }
    IMissionService MissionService { get; } 
    IGoalMissionService GoalMissionService { get; }
    IMissionMediaService MissionMediaService { get; }
    IMissionSkillService MissionSkillService { get; }
    IMissionRatingService MissionRatingService { get; }

    IMissionInviteService MissionInviteService { get; }
    ICommentService CommentService { get; }
    IStoryService StoryService { get; }

    IStoryMediaService StoryMediaService { get; }

    IStoryInviteService StoryInviteService { get; }

    IUserSkillService UserSkillService { get; }

    IContactUsService ContactUsService { get; } 

    ITimesheetService TimesheetService { get; }

    ICmsPageService CmsPageService { get; }

    IVerifyEmailService VerifyEmailService { get; }

    IBannerService BannerService { get; }
}
