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

    public MissionCardVM ConvertMissionToMissionCardVM( Mission mission)
    {
        MissionCardVM missionCard = new()
        {
            MissionId = mission.MissionId,
            ThemeId = mission.ThemeId,
            ThemeName = mission.Theme.Title,
            Title = mission.Title,
            ShortDescription = mission.ShortDescription,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            MissionType = mission.MissionType! ? MissionTypeEnum.TIME : MissionTypeEnum.GOAL,
            Status = (bool)mission.Status ? MissionStatus.ONGOING : MissionStatus.FINISHED,
            OrganizationName = mission.OrganizationName,
            TotalSeat = mission.TotalSeat,
            NumberOfVolunteer = mission.MissionApplications?.Where( application => application.ApprovalStatus == 1 ).LongCount(),
            SeatLeft = mission?.TotalSeat - mission?.MissionApplications.LongCount(),
            RegistrationDeadline = mission?.RegistrationDeadline,
            Rating = mission?.Rating,
            CityId = mission?.CityId,
            CityName = mission?.City?.Name,
            ThumbnailUrl = GetThumbnailUrl( mission.MissionMedia ),
            GoalValue = mission.GoalMissions?.FirstOrDefault(msnGoal => msnGoal.GoalValue != 0)?.GoalValue,
            GoalText = mission?.GoalMissions?.FirstOrDefault(goal => goal.GoalObjectiveText != null)?.GoalObjectiveText,
            GoalAchieved = mission?.GoalMissions?.FirstOrDefault(goal => goal.GoalAchived != null)?.GoalAchived
        };

        return missionCard;
    }

   
    private string? GetThumbnailUrl(ICollection<MissionMedium> missionMedia)
    {
        if (missionMedia == null) return null;

        MissionMedium media = missionMedia.FirstOrDefault( med => med.Default );

        string? path = media?.MediaPath;
        string? name = media?.MediaName;
        string? extension = media?.MediaType;

        return path + name + extension;
    }
}
