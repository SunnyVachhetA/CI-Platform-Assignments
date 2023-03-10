
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Services.Service.Interface;

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

    public ServiceUnit(IUnitOfWork unitOfWork)
	{
		_unitOfWork= unitOfWork;
		UserService = new UserService(_unitOfWork);
		PasswordResetService = new PasswordResetService(_unitOfWork);
		CountryService = new CountryService(_unitOfWork);
		CityService = new CityService(_unitOfWork);
		SkillService = new SkillService(_unitOfWork);
		ThemeService = new ThemeService(_unitOfWork);
        MissionApplicationService = new MissionApplicationService(_unitOfWork);
        FavouriteMissionService = new FavouriteMissionService(_unitOfWork);
        MissionService = new MissionService(_unitOfWork);
        GoalMissionService = new GoalMissionService(_unitOfWork);
        MissionMediaService = new MissionMediaService(_unitOfWork);
        MissionSkillService = new MissionSkillService(_unitOfWork);
	}
}
