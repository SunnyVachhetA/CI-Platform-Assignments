using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IUnitOfWork
{
    IUserRepository UserRepo { get; }
    IPasswordResetRepository PasswordResetRepo { get; }
    ICountryRepository CountryRepo { get; }
    ICityRepository CityRepo { get; }
    ISkillRepository SkillRepo { get; }
    IThemeRepository ThemeRepo { get; }
    IMissionRepository MissionRepo { get; }
    IFavouriteMissionRepository FavouriteMissionRepo { get; }
    IMissionApplicationRepository MissionApplicationRepo { get; }
    IGoalMissionRepository GoalMissionRepo { get; }
    IMissionMediaRepository MissionMediaRepo { get; }
    IMissionSkillRepository MissionSkillRepo { get; }
    IMissionRatingRepository MissionRatingRepo { get; }
    ICommentRepository CommentRepo { get; }

    IMissionInviteRepository MissionInviteRepo { get; }
    IStoryRepository StoryRepo { get; } 
    IStoryMediaRepository StoryMediaRepo { get; }

    IStoryInviteRepository StoryInviteRepo { get; }

    IUserSkillRepository UserSkillRepo { get; }

    IContactUsRepository ContactUsRepo { get; }

    ITimesheetRepository TimesheetRepo { get; }

    ICmsPageRepository CmsPageRepo { get; }

    IVerifyEmailRepository VerifyEmailRepo { get; }

    IBannerRepository BannerRepo { get; }

    IMissionDocumentRepository MissionDocumentRepo { get; } 

    IUserNotificationCheckRepository UserNotificationCheckRepo { get; }
    INotificationSettingRepository NotificationSettingRepo { get; }

    IUserNotificationRepository UserNotificationRepo { get; }
    void Save();

    Task<IDbContextTransaction> BeginTransactionAsync();

    Task<int> SaveAsync();
}
