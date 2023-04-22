
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Services.Service.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace CIPlatform.Services.Service;
public class ServiceUnit: IServiceUnit
{
    private readonly IUnitOfWork _unitOfWork;
    public IUserService UserService { get; private set; }
    public IPasswordResetService PasswordResetService { get; private set; }
	public ICountryService CountryService { get; private set; }
	public ICityService CityService { get; private set; }
    public ISkillService SkillService { get; private set; }
    public IThemeService ThemeService { get; private set; }

    public IMissionApplicationService MissionApplicationService { get; private set; }

    public IFavouriteMissionService FavouriteMissionService { get; private set; }

    public IMissionService MissionService { get; private set; }

    public IGoalMissionService GoalMissionService { get; private set; }

    public IMissionMediaService MissionMediaService { get; private set; }

    public IMissionSkillService MissionSkillService { get; private set; }

    public IMissionRatingService MissionRatingService {  get; private set; }

    public ICommentService CommentService { get; private set; }

    public IMissionInviteService MissionInviteService { get; private set; }

    public IStoryService StoryService { get; private set; }

    public IStoryMediaService StoryMediaService { get; private set; }
    public IStoryInviteService StoryInviteService { get; private set; }

    public IUserSkillService UserSkillService { get; private set; }

    public IContactUsService ContactUsService { get; private set; }
    public ITimesheetService TimesheetService { get; private set; }
    public ICmsPageService CmsPageService { get; private set; }

    public IVerifyEmailService VerifyEmailService { get; private set; }
    public IBannerService BannerService { get; private set; }

    public ServiceUnit(IUnitOfWork unitOfWork, IEmailService emailService)
	{
		_unitOfWork= unitOfWork;
		UserService = new UserService(_unitOfWork, emailService);
		PasswordResetService = new PasswordResetService(_unitOfWork);
		CountryService = new CountryService(_unitOfWork);
		CityService = new CityService(_unitOfWork);
		SkillService = new SkillService(_unitOfWork);
        MissionApplicationService = new MissionApplicationService(_unitOfWork);
        FavouriteMissionService = new FavouriteMissionService(_unitOfWork);
        MissionService = new MissionService(_unitOfWork);
        GoalMissionService = new GoalMissionService(_unitOfWork);
        MissionMediaService = new MissionMediaService(_unitOfWork);
        MissionSkillService = new MissionSkillService(_unitOfWork);
        MissionRatingService = new MissionRatingService(_unitOfWork);
        CommentService = new CommentService(_unitOfWork);
        MissionInviteService = new MissionInviteService(_unitOfWork);
        StoryService = new StoryService(_unitOfWork);
        StoryMediaService = new StoryMediaService(_unitOfWork);
        StoryInviteService = new StoryInviteService(_unitOfWork, emailService);
        UserSkillService = new UserSkillService(_unitOfWork);
        ContactUsService = new ContactUsService(_unitOfWork, emailService);
        TimesheetService = new TimesheetService(_unitOfWork);
        CmsPageService = new CmsPageService(_unitOfWork);
		ThemeService = new ThemeService(_unitOfWork);
        VerifyEmailService = new VerifyEmailService(_unitOfWork);
        BannerService = new BannerService(_unitOfWork);
    }
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _unitOfWork.BeginTransactionAsync();
    }
}
