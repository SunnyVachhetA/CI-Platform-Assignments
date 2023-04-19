using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using Microsoft.EntityFrameworkCore;

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
        var goalObj = mission.GoalMissions?.FirstOrDefault(msnGoal => msnGoal.GoalValue != 0);
        var missionCard = new MissionCardVM
        {
            MissionId = mission.MissionId,
            ThemeId = mission.ThemeId,
            ThemeName = mission.Theme?.Title,
            Title = mission.Title,
            ShortDescription = mission.ShortDescription,
            Description = mission.Description,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            MissionType = mission.MissionType == true ? MissionTypeEnum.TIME : MissionTypeEnum.GOAL,
            OrganizationDetails = mission.OrganizationDetail,
            Status = mission.Status == true ? MissionStatus.ONGOING : MissionStatus.FINISHED,
            OrganizationName = mission.OrganizationName,
            TotalSeat = mission.TotalSeat,
            NumberOfVolunteer = mission.MissionApplications?.Count(application => application.ApprovalStatus == 1),
            SeatLeft = mission?.TotalSeat - mission?.MissionApplications?.Count(),
            RegistrationDeadline = mission?.RegistrationDeadline,
            Rating = mission?.Rating,
            CityId = mission?.CityId,
            CityName = mission?.City?.Name,
            CountryId = mission.CountryId,
            SkillId = mission.MissionSkills.Select(skill => skill.SkillId).ToList(),
            Skills = mission.MissionSkills.Select(skill => skill?.Skill?.Name).ToList(),
            ThumbnailUrl = GetThumbnailUrl(mission.MissionMedia.FirstOrDefault(media => media.Default)),
            MissionMedias = mission.MissionMedia?.Select(GetThumbnailUrl).ToList(),
            FavrouriteMissionsId = mission.FavouriteMissions?.Select(fav => fav.UserId).ToList(),
            GoalValue = goalObj?.GoalValue,
            GoalText = goalObj?.GoalObjectiveText,
            GoalAchieved = goalObj?.GoalAchived,
            ApplicationListId = mission.MissionApplications?.Where(application => application.ApprovalStatus == 1).Select(application => (long)application.UserId).ToList(),
            MissionRating = MissionRatingService.ConvertMissionToRatingVM(mission),
            MissionAvailability = SetMissionAvailability(mission.Availability),
            CommentList = mission.Comments?.Select(comment => comment.UserId).ToList(),
            RecentVolunteers = GetRecentVolunteers(mission),
            MissionDocuments = GetMissionDocuments(mission.MissionDocuments)
        };

        return missionCard;
    }

    private static List<MissionDocumentVM> GetMissionDocuments(ICollection<MissionDocument> missionDocuments)
    {
        IEnumerable<MissionDocumentVM> result = missionDocuments
                    .Select
                    (
                        document =>
                            new MissionDocumentVM
                            {
                                DocumentId = document.MissionDocumentId,
                                DocumentPath = GetDocumentPath( document ),
                                Title = $"{document.DocumentName}.{document.DocumentType}"
                            }
                    );
        return result.ToList();
    }

    private static string GetDocumentPath(MissionDocument document)
    {
        string path = $"{document.DocumentPath}{document.DocumentName}.{document.DocumentType}";

        return path;
    }

    private static List<RecentVolunteersVM> GetRecentVolunteers(Mission mission)
    {
        List<RecentVolunteersVM> volunteerList = new();
        var result = GetApprovedMissionApplication(mission);
        if (result.LongCount() > 0)
        {
            result = result.OrderByDescending(missionApplication => missionApplication.CreatedAt);

            volunteerList = result.Select( missionApplication => 
                                            new RecentVolunteersVM  { 
                                                UserId = missionApplication.UserId,
                                                Avatar = missionApplication.User.Avatar?? string.Empty,
                                                UserName = missionApplication.User.FirstName + " " + missionApplication.User.LastName
                                            }
                                        ).ToList();
        }
        return volunteerList;
    }

    private static IEnumerable<MissionApplication> GetApprovedMissionApplication( Mission mission )
    {
        if (mission.MissionApplications.Any())
        {
            var result = mission.MissionApplications
                            .Where(missionApplication => missionApplication.ApprovalStatus == 1);
            return result;
        }
        return new List<MissionApplication>().AsEnumerable();
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
        
        FilterService filter = new FilterService(result, filterModel);

        result = filter.FilterCriteria();
        
        return result.ToList();
    }

    public (IEnumerable<MissionVMCard>, long) FilterMissionsCard(FilterModel filterModel)
    {
        var result = unitOfWork.MissionRepo.FetchMissionCardInformation();

        var missionList =
            result
                .Select(ConvertMissionToMissionVMCard);

        if (missionList == null || !missionList.Any()) return (new List<MissionVMCard>(), 0);
        FilterMissionService filter = new FilterMissionService(missionList, filterModel);
        long totalMissionCount = 0;
        var filteredMissions = filter.Filter(out totalMissionCount);

        return (filteredMissions, totalMissionCount);
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

    public bool IsThemeUsedInMission(short themeId)
    {
        return
            unitOfWork
                .MissionRepo
                .GetAll()
                .Any(msn => msn.ThemeId == themeId);
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

    /// <summary>
    /// This method is replace of 
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public (IEnumerable<MissionVMCard>, long ) LoadAllMissionCards(int page)
    {
        var missions = unitOfWork.MissionRepo.FetchMissionCardInformation();

        if (!missions.Any()) return (new List<MissionVMCard>(), 0);

        var totalMissionCount = missions.LongCount();

        IEnumerable<MissionVMCard> missionList =
            missions
                .Skip((page - 1) * 9)
                .Take(9)
                .Select(ConvertMissionToMissionVMCard);

        return (missionList, totalMissionCount);
    }

    
    private static MissionVMCard ConvertMissionToMissionVMCard(Mission mission)
    {
        var missionApplication = mission.MissionApplications;
        var missionApprovedApplication = 
            missionApplication
            ?.Where(application => application.ApprovalStatus == 1)
            .ToList();
        var msnGoal = mission.GoalMissions.FirstOrDefault( goal => goal.GoalValue != 0 );
        MissionVMCard vmCard = new()
        {
            MissionId = mission.MissionId,
            ThemeId = mission.ThemeId ?? 0,
            ThemeName = mission.Theme?.Title?? string.Empty,
            Title = mission.Title ?? string.Empty,
            ShortDescription = mission.ShortDescription ?? string.Empty,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            MissionType = mission.MissionType ? MissionTypeEnum.TIME : MissionTypeEnum.GOAL,
            Status = mission.Status == true ? MissionStatus.ONGOING : MissionStatus.FINISHED,
            OrganizationName = mission.OrganizationName,
            TotalSeat = mission.TotalSeat,
            NumberOfVolunteer = missionApprovedApplication?.LongCount() ?? 0,
            SeatLeft = mission.TotalSeat - missionApprovedApplication?.LongCount() ?? 0,
            ApplicationListId = missionApplication?.Select( application => application.UserId ) ?? new List<long>(),
            ApprovedApplicationList = missionApprovedApplication?.Select( application => application.UserId ) ?? new List<long>(),
            RegistrationDeadline = mission.RegistrationDeadline,
            Rating = GetMissionRating(mission.MissionRatings),
            CityName = mission.City?.Name ?? string.Empty,
            ThumbnailUrl = GetThumbnailUrl(mission.MissionMedia?.FirstOrDefault(media => media.Default)!),
            FavoriteMissionList =  mission.FavouriteMissions?.Select(msn => msn.UserId) ?? new List<long>(),
            GoalText = msnGoal?.GoalObjectiveText,
            GoalValue = msnGoal?.GoalValue,
            GoalAchieved = msnGoal?.GoalAchived,
            MissionSkill = mission.MissionSkills?.Select(skill => skill.SkillId) ?? new List<short>(),
            CountryId = mission.CountryId,
            CityId = mission.CityId
        };

        return vmCard;
    }

    private static byte GetMissionRating(ICollection<MissionRating> missionRating)
    {
        if (!missionRating.Any()) return 0;

        return (byte)Math.Floor(missionRating.Average(rating => rating.Rating));
    }
}
/*
 * MissionCardVM missionCard = new()
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
            MissionAvailability = SetMissionAvailability(mission.Availability),
            CommentList = mission.Comments?.Select(comment => comment.UserId).ToList(),
            RecentVolunteers = GetRecentVolunteers(mission),
            MissionDocuments = GetMissionDocuments( mission.MissionDocuments )
        };
 */