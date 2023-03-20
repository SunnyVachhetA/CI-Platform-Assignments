using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class MissionService : IMissionService
{
    private IUnitOfWork unitOfWork;
    public MissionService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    

    //Get all mission card details
    public List<MissionCardVM> GetAllMissionCards()
    {
        //List<Mission> missions = unitOfWork.MissionRepo.GetAll().ToList();
        List<Mission> missions = unitOfWork.MissionRepo.GetAllMissions().ToList();

        List<MissionCardVM> missionList = new();

        foreach( var mission in missions )
        {
            missionList.Add( ConvertMissionToMissionCardVM( mission ) );
        }

        return missionList;
    }

    public static MissionCardVM ConvertMissionToMissionCardVM(Mission mission)
    {
        MissionCardVM missionCard = new()
        {
            MissionId = mission.MissionId,
            ThemeId = mission.ThemeId,
            ThemeName = mission.Theme?.Title,
            Title = mission.Title,
            ShortDescription = mission.ShortDescription,
            Description = mission.Description,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            MissionType = mission.MissionType! ? MissionTypeEnum.TIME : MissionTypeEnum.GOAL,
            OrganizationDetails = mission.OrganizationDetail,
            Status = (bool)mission.Status ? MissionStatus.ONGOING : MissionStatus.FINISHED,
            OrganizationName = mission.OrganizationName,
            TotalSeat = mission.TotalSeat,
            NumberOfVolunteer = mission.MissionApplications?.Where(application => application.ApprovalStatus == 1).LongCount(),
            SeatLeft = mission?.TotalSeat - mission?.MissionApplications.LongCount(),
            RegistrationDeadline = mission?.RegistrationDeadline,
            Rating = mission?.Rating,
            CityId = mission?.CityId,
            CityName = mission?.City?.Name,
            CountryId = mission.CountryId,
            SkillId = mission.MissionSkills.Select(skill => skill.SkillId).ToList(),
            Skills = (List<string>)mission.MissionSkills.Select(skill => skill?.Skill?.Name).ToList(),
            ThumbnailUrl = GetThumbnailUrl(mission.MissionMedia.FirstOrDefault(media => media.Default)),
            MissionMedias = mission.MissionMedia?.Select(media => GetThumbnailUrl(media)).ToList(),
            FavrouriteMissionsId = mission.FavouriteMissions?.Select(fav => fav.UserId).ToList(),
            GoalValue = mission.GoalMissions?.FirstOrDefault(msnGoal => msnGoal.GoalValue != 0)?.GoalValue,
            GoalText = mission?.GoalMissions?.FirstOrDefault(goal => goal.GoalObjectiveText != null)?.GoalObjectiveText,
            GoalAchieved = mission?.GoalMissions?.FirstOrDefault(goal => goal.GoalAchived != null)?.GoalAchived,
            ApplicationListId = mission.MissionApplications?.Where(application => application.ApprovalStatus == 1).Select(application => (long)application?.UserId).ToList(),
            MissionRating = MissionRatingService.ConvertMissionToRatingVM(mission),
            MissionAvailability = SetMissionAvailability(mission.Availability) 
        };

        return missionCard;
    }

    private static MissionAvailability? SetMissionAvailability(byte? availability)
    {
        if (availability == null || availability == 0) return null;

        MissionAvailability? missionAvail = null;
        switch( availability )
        {
            case 1:
                missionAvail = MissionAvailability.DAILY;
                break;
            case 2:
                missionAvail = MissionAvailability.WEEK_END; break;
            case 3:
                missionAvail = MissionAvailability.WEEKLY; break;
            case 4:
                missionAvail = MissionAvailability.MONTHLY; break;
        }
        return missionAvail;
    }

    private static string? GetThumbnailUrl(MissionMedium missionMedia)
    {
        if (missionMedia == null) return null;

        string? path = missionMedia?.MediaPath;
        string? name = missionMedia?.MediaName;
        string? extension = missionMedia?.MediaType;

        return path + name + extension;
    }

    //Get mission details by id
    public MissionCardVM LoadMissionDetails(long id)
    {
        var mission = unitOfWork.MissionRepo.FetchMissionDetailsById(id);
        MissionCardVM missionVM = ConvertMissionToMissionCardVM( mission );
        return missionVM; 
    }

    //Method for filtering out mission based on user filter selection
    public List<MissionCardVM> FilterMissions(FilterModel filterModel)
    {
        var result = GetAllMissionCards().AsQueryable() ;
        /*if (filterModel.SortBy.HasValue)
        {
            if (filterModel.SortBy == SortByMenu.RESET)
            {
                return result.ToList();
            }
        }*/
        
        FilterService filter = new FilterService(result, filterModel);

        result = filter.FilterCriteria();
        
        return result.ToList();
    }

    public MissionLandingVM CreateMissionLanding( )
    {
        ICountryService countryService = new CountryService(unitOfWork);
        ICityService cityService = new CityService(unitOfWork); 
        ISkillService skillService = new SkillService(unitOfWork);
        IThemeService themeService = new ThemeService(unitOfWork);
        var countryList = countryService.GetAllCountry();
        var cityList = cityService.GetAllCities();
        var themeList = themeService.GetAllThemes();
        var skillList = skillService.GetAllSkills();
        //var missionList = GetAllMissionCards();
        MissionLandingVM missionLanding = new()
        {
            countryList = countryList,
            cityList = cityList,
            themeList = themeList,
            skillList = skillList,
            //missionList = missionList
        };
        return missionLanding;
    }

    public MissionLandingVM CreateMissionLanding(List<MissionCardVM> missionList)
    {
        MissionLandingVM missionLanding = new()
        {
            missionList = missionList
        };
        return missionLanding;
    }

    public Task UpdateMissionRating(long missionId, byte avgRating)
    {
        unitOfWork.MissionRepo.UpdateMissionRating(missionId, avgRating);;
        return Task.CompletedTask;
    }

    //Load related mission based on theme
    public List<MissionCardVM> LoadRelatedMissionBasedOnTheme(short themeId, long missionId)
    {
        var missions =  unitOfWork.MissionRepo?.FetchMissionInformation()?
            .Where( mission => ( mission.ThemeId == themeId && mission.MissionId != missionId) ).ToList();

        List<MissionCardVM> result = new();
        
        foreach(var msn in missions)
        {
            result.Add( ConvertMissionToMissionCardVM(msn) );
        }
        return result;
    }
}
