using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;

namespace CIPlatform.Services.Service;
public class MissionService : IMissionService
{
    private IUnitOfWork unitOfWork;
    private readonly IMissionMediaService _missionMediaService;
    private readonly IMissionDocumentService _missionDocumentService;
    private readonly IMissionSkillService _missionSkillService;
    private readonly IGoalMissionService _goalMissionService;
    public MissionService(IUnitOfWork unitOfWork, IMissionMediaService missionMediaService,
        IMissionDocumentService missionDocumentService, IMissionSkillService missionSkillService, IGoalMissionService goalMissionService)
    {
        this.unitOfWork = unitOfWork;
        _missionMediaService = missionMediaService;
        _missionDocumentService = missionDocumentService;
        _missionSkillService = missionSkillService;
        _goalMissionService = goalMissionService;
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
            MissionDocuments = GetMissionDocuments(mission.MissionDocuments),
            ApplicationList = mission.MissionApplications!.Select( app => app.UserId )
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
                                Path = GetDocumentPath( document ),
                                Title = $"{document.DocumentTitle}{document.DocumentType}"
                            }
                    );
        return result.ToList();
    }

    private static string GetDocumentPath(MissionDocument document)
    {
        string path = $"{document.DocumentPath}{document.DocumentName}{document.DocumentType}";

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

    public async Task<(IEnumerable<MissionVMCard>, long)> FilterMissionsCard(FilterModel filterModel)
    {
        var result = await unitOfWork.MissionRepo.FetchMissionCardInformationAsync();

        var missionList =
            result
                .OrderBy(msn => msn.CreatedAt)
                .Select(ConvertMissionToMissionVMCard);

        if (missionList == null || !missionList.Any()) return (new List<MissionVMCard>(), 0);

        FilterMissionService filter = new FilterMissionService(missionList.AsQueryable(), filterModel);
        
        var filteredMissions =  filter.Filter();

        return (filteredMissions.Item1, filteredMissions.Item2);
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
        var start = System.Diagnostics.Stopwatch.StartNew();
        start.Start();
        var missions = unitOfWork.MissionRepo.FetchMissionCardInformation();
        start.Stop();
        var elapsed = start.ElapsedMilliseconds;
        Console.WriteLine("Elapsed1: " + elapsed);
        //if (!missions.Any()) return (new List<MissionVMCard>(), 0);
        start.Reset();
        start.Start();
        long totalMissionCount = missions.LongCount();
        start.Stop();
        elapsed = start.ElapsedMilliseconds;
        Console.WriteLine("Elapsed2: " + elapsed);


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
            CityId = mission.CityId,
            CreatedAt = mission.CreatedAt
        };

        return vmCard;
    }

    private static byte GetMissionRating(ICollection<MissionRating> missionRating)
    {
        if (!missionRating.Any()) return 0;

        return (byte)Math.Floor(missionRating.Average(rating => rating.Rating));
    }
    
    //Admin methods
    public IEnumerable<AdminMissionVM> LoadAllMissionsAdmin()
    {
        var missions = unitOfWork.MissionRepo.GetAll();
        return
            missions
                .OrderByDescending(msn => msn.CreatedAt)
                .Select(ConvertToMissionAdminVM);
    }

    public IEnumerable<AdminMissionVM> SearchMission(string searchKey)
    {
        if (string.IsNullOrEmpty(searchKey) || string.IsNullOrWhiteSpace(searchKey)) return LoadAllMissionsAdmin();
        Func<Mission, bool> filter = msn => msn.Title!.ContainsCaseInsensitive(searchKey);
        return
            unitOfWork.MissionRepo.GetAll(filter)
                .Select(ConvertToMissionAdminVM);
    }

    public async Task CreateTimeMission(TimeMissionVM mission, string wwwRootPath)
    {
        using (var transaction = await unitOfWork.BeginTransactionAsync())
        {
            try
            {
                Mission entity = ConvertTimeVMToMission(mission);
                await unitOfWork.MissionRepo.AddAsync(entity);
                await unitOfWork.SaveAsync();

                await _missionSkillService.SaveMissionSkills(mission.Skills, entity.MissionId);
                await _missionMediaService.StoreMissionMedia(mission.Images, wwwRootPath, entity.MissionId);
                await _missionDocumentService.StoreMissionDocument(mission.Documents!, wwwRootPath, entity.MissionId);
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while creating mission: " + e.Message);
                Console.WriteLine(e.StackTrace);
                await transaction.RollbackAsync();
                throw;
            }
        }
        
    }

    public async Task CreateGoalMission(GoalMissionVM mission, string wwwRootPath)
    {
        using (var transaction = await unitOfWork.BeginTransactionAsync())
        {
            try
            {
                Mission entity = ConvertGoalVMToMission(mission);
                await unitOfWork.MissionRepo.AddAsync(entity);
                await unitOfWork.SaveAsync();
                
                var saveMissionGoalDetails = _goalMissionService.SaveMissionGoalDetails(mission, entity.MissionId);
                var saveMissionSkillsTask = _missionSkillService.SaveMissionSkills(mission.Skills, entity.MissionId);
                var storeMissionMediaTask = _missionMediaService.StoreMissionMedia(mission.Images, wwwRootPath, entity.MissionId);
                var storeMissionDocumentTask = _missionDocumentService.StoreMissionDocument(mission.Documents!, wwwRootPath, entity.MissionId);

                await Task.WhenAll(saveMissionGoalDetails, saveMissionSkillsTask, storeMissionMediaTask, storeMissionDocumentTask);
                await transaction.CommitAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error while creating goal mission: " + e.Message);
                Console.WriteLine(e.StackTrace);
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task<TimeMissionVM> LoadEditTimeMissionDetails(long id)
    {
        try
        {
            var mission = await unitOfWork.MissionRepo.FetchMissionWithMedia(id);
            if (mission == null) throw new Exception("Mission not found: " + id);

            var vm = ConvertMissionToTimeVM(mission);

            vm.MediaList = await _missionMediaService.ConvertMediaToMediaVM(mission.MissionMedia);
            vm.DocumentList = await _missionDocumentService.ConvertToDocumentVM(mission.MissionDocuments);
            return vm;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while editing time mission load: " + e.Message);
            Console.WriteLine(e.StackTrace);
            throw;
        }
    }

    public async Task UpdateTimeMission(TimeMissionVM mission, IEnumerable<string> preloadedMediaList,
        IEnumerable<string> preloadedDocumentPathList,
        IEnumerable<short> preloadedSkill, string wwwRootPath)
    {
        using (var transaction = await unitOfWork.BeginTransactionAsync())
        {
            try
            {
                await _missionSkillService.EditMissionSkill(mission.MissionId, mission.Skills, preloadedSkill);
                await _missionMediaService.EditMissionMedia(mission.MissionId, mission.Images, preloadedMediaList, wwwRootPath);
                await _missionDocumentService.EditMissionDocument(mission.MissionId, mission.Documents, preloadedDocumentPathList, wwwRootPath);
                var entity = await unitOfWork.MissionRepo.GetFirstOrDefaultAsync( msn => msn.MissionId == mission.MissionId );

                entity.Title = mission.Title;
                entity.ShortDescription = mission.ShortDescription;
                entity.Description = mission.Description;
                entity.OrganizationName = mission.OrganizationName;
                entity.OrganizationDetail = mission.OrganizationDetail;
                entity.ThemeId = mission.ThemeId;
                entity.StartDate = mission.StartDate;
                entity.EndDate = mission.EndDate;
                entity.CityId = mission.CityId;
                entity.CountryId = mission.CountryId;
                entity.TotalSeat = mission.TotalSeats;
                entity.RegistrationDeadline = mission.RegistrationDeadline;
                entity.Availability = (byte)mission.Availability;
                entity.Status = mission.IsActive;
                
                await unitOfWork.SaveAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while editing time mission: " + e.Message);
                Console.WriteLine(e.StackTrace);
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task<GoalMissionVM> LoadEditGoalMissionDetails(long id)
    {
        try
        {
            var mission = await unitOfWork.MissionRepo.FetchMissionWithMediaGoal(id);
            if (mission == null) throw new Exception("Mission not found: " + id);

            var vm = ConvertMissionToGoalVM(mission);

            vm.MediaList = await _missionMediaService.ConvertMediaToMediaVM(mission.MissionMedia);
            vm.DocumentList = await _missionDocumentService.ConvertToDocumentVM(mission.MissionDocuments);

            return vm;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while editing goal mission load: " + e.Message);
            Console.WriteLine(e.StackTrace);
            throw;
        }
    }

    

    public async Task UpdateGoalMission(GoalMissionVM mission, IEnumerable<string> preloadedMediaList, IEnumerable<string> preloadedDocumentPathList,
        IEnumerable<short> preloadedSkill, string wwwRootPath)
    {
        using (var transaction = await unitOfWork.BeginTransactionAsync())
        {
            try
            {
                await _missionSkillService.EditMissionSkill(mission.MissionId, mission.Skills, preloadedSkill);
                await _missionMediaService.EditMissionMedia(mission.MissionId, mission.Images, preloadedMediaList, wwwRootPath);
                await _missionDocumentService.EditMissionDocument(mission.MissionId, mission.Documents, preloadedDocumentPathList, wwwRootPath);
                var entity = await unitOfWork.MissionRepo.FetchMissionWithMediaGoal(mission.MissionId);
                await _goalMissionService.EditGoalMissionDetails(entity.GoalMissions.First(), mission.GoalValue, mission.GoalObjective);

                entity.Title = mission.Title;
                entity.ShortDescription = mission.ShortDescription;
                entity.Description = mission.Description;
                entity.OrganizationName = mission.OrganizationName;
                entity.OrganizationDetail = mission.OrganizationDetail;
                entity.ThemeId = mission.ThemeId;
                entity.StartDate = mission.StartDate;
                entity.EndDate = mission.EndDate;
                entity.CityId = mission.CityId;
                entity.CountryId = mission.CountryId;
                entity.TotalSeat = mission.TotalSeats;
                entity.RegistrationDeadline = mission.RegistrationDeadline;
                entity.Availability = (byte)mission.Availability;
                entity.Status = mission.IsActive;

                await unitOfWork.SaveAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while editing goal mission: " + e.Message);
                Console.WriteLine(e.StackTrace);
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    private static GoalMissionVM ConvertMissionToGoalVM(Mission mission)
    {
        var goal = mission.GoalMissions.First();
        GoalMissionVM goalVM = new GoalMissionVM()
        {
            MissionId = mission.MissionId,
            ThemeId = mission.ThemeId,
            CountryId = mission.CountryId ?? 0,
            CityId = mission.CityId ?? 0,
            Title = mission.Title!,
            ShortDescription = mission.ShortDescription!,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            IsActive = mission.IsActive,
            OrganizationName = mission.OrganizationName!,
            OrganizationDetail = mission.OrganizationDetail,
            Availability = (MissionAvailability)(mission.Availability ?? 0),
            TotalSeats = mission.TotalSeat,
            RegistrationDeadline = mission.RegistrationDeadline,
            Description = mission.Description!,
            Skills = mission.MissionSkills.Select(sk => sk.SkillId),
            GoalObjective = goal.GoalObjectiveText?? string.Empty,
            GoalValue = goal.GoalValue
        };
        return goalVM;
    }

    public Mission ConvertTimeVMToMission(TimeMissionVM mission)
    {
        Mission entity = new()
        {
            ThemeId = mission.ThemeId,
            CountryId = mission.CountryId,
            CityId = mission.CityId,
            Title = mission.Title,
            ShortDescription = mission.ShortDescription,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            MissionType = true,
            IsActive = mission.IsActive,
            OrganizationName = mission.OrganizationName,
            OrganizationDetail = mission.OrganizationDetail,
            Availability = (byte)mission.Availability,
            TotalSeat = mission.TotalSeats,
            RegistrationDeadline = mission.RegistrationDeadline,
            CreatedAt = DateTimeOffset.Now,
            Description = mission.Description,
            Status = true
        };

        return entity;
    }
    public TimeMissionVM ConvertMissionToTimeVM(Mission mission)
    {
        TimeMissionVM timeMissionVM = new TimeMissionVM
        {
            MissionId = mission.MissionId,
            ThemeId = mission.ThemeId,
            CountryId = mission.CountryId?? 0,
            CityId = mission.CityId?? 0,
            Title = mission.Title!,
            ShortDescription = mission.ShortDescription!,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            IsActive = mission.IsActive,
            OrganizationName = mission.OrganizationName!,
            OrganizationDetail = mission.OrganizationDetail,
            Availability = (MissionAvailability)(mission.Availability?? 0),
            TotalSeats = mission.TotalSeat,
            RegistrationDeadline = mission.RegistrationDeadline,
            Description = mission.Description!,
            Skills = mission.MissionSkills.Select( sk => sk.SkillId )
        };

        return timeMissionVM;
    }
    public Mission ConvertGoalVMToMission(GoalMissionVM mission)
    {
        Mission entity = new()
        {
            ThemeId = mission.ThemeId,
            CountryId = mission.CountryId,
            CityId = mission.CityId,
            Title = mission.Title,
            ShortDescription = mission.ShortDescription,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            MissionType = false,
            IsActive = mission.IsActive,
            OrganizationName = mission.OrganizationName,
            OrganizationDetail = mission.OrganizationDetail,
            Availability = (byte)mission.Availability,
            TotalSeat = mission.TotalSeats,
            RegistrationDeadline = mission.RegistrationDeadline,
            CreatedAt = DateTimeOffset.Now,
            Description = mission.Description,
            Status = true
        };

        return entity;
    }
    private static AdminMissionVM ConvertToMissionAdminVM(Mission mission)
    {
        AdminMissionVM vm = new()
        {
            MissionId = mission.MissionId,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            Title = mission.Title?? string.Empty,
            MissionType = (MissionTypeEnum) ( mission.MissionType ? 1 : 0),
            IsActive = mission.IsActive?? true,
            MissionStatus = (mission.Status??true) ? MissionStatus.ONGOING : MissionStatus.FINISHED
        };
        return vm;
    }
    public async Task<(IEnumerable<MissionVMCard>, long)> LoadAllMissionCardsAsync(int page)
    {
        var missions = await unitOfWork.MissionRepo.FetchMissionCardInformationAsync();

        long totalMissionCount = missions.LongCount();

        IEnumerable<MissionVMCard> missionList = missions
            .OrderByDescending(msn => msn.CreatedAt)
            .Skip((page - 1) * 9)
            .Take(9)
            .Select(ConvertMissionToMissionVMCard);

        return (missionList, totalMissionCount);
    }

    public async Task UpdateMissionActiveStatus(long id, byte status = 0)
    {
        int result = await unitOfWork.MissionRepo.UpdateMissionActiveStatus(id, status);
        if (result == 0) throw new Exception("Something went wrong during updating mission activation status!!");
    }

    public async Task UpdateMissionCloseStatus(long id, int status)
    {
        int result = await unitOfWork.MissionRepo.UpdateMissionCloseStatus(id, status);
        if (result == 0) throw new Exception("Something went wrong during updating mission close status!!");
    }
}
