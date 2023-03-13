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

    public MissionCardVM ConvertMissionToMissionCardVM(Mission mission)
    {
        MissionCardVM missionCard = new()
        {
            MissionId = mission.MissionId,
            ThemeId = mission.ThemeId,
            ThemeName = mission.Theme.Title,
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
            ThumbnailUrl = GetThumbnailUrl(mission.MissionMedia.FirstOrDefault( media => media.Default ) ),
            MissionMedias = mission.MissionMedia?.Select( media => GetThumbnailUrl( media ) ).ToList(),
            GoalValue = mission.GoalMissions?.FirstOrDefault(msnGoal => msnGoal.GoalValue != 0)?.GoalValue,
            GoalText = mission?.GoalMissions?.FirstOrDefault(goal => goal.GoalObjectiveText != null)?.GoalObjectiveText,
            GoalAchieved = mission?.GoalMissions?.FirstOrDefault(goal => goal.GoalAchived != null)?.GoalAchived
        };

        return missionCard;
    }
   
    private string? GetThumbnailUrl(MissionMedium missionMedia)
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

    public List<MissionCardVM> LoadRelatedMissionBasedOnTheme(int? themeId)
    {
        var missions = unitOfWork.MissionRepo.FetchRelatedMissionsByTheme( themeId );
        return null!;
    }
}
