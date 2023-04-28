using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;

namespace CIPlatform.Services.Service;
public class MissionApplicationService : IMissionApplicationService
{
    private IUnitOfWork unitOfWork;

    public MissionApplicationService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public Task<List<RecentVolunteersVM>> FetchRecentVolunteers(long missionId)
    {
        var result = unitOfWork
                            .MissionApplicationRepo
                            .FetchRecentVolunteersInformation(missionId );
        var recentVolunteerList = new List<RecentVolunteersVM>();
        if(result.Any())
        {
            recentVolunteerList = result.Select( application => ConvertMissionApplicationToRecentVolunteerVM(application) ).ToList();
        }
        return Task.Run( () => recentVolunteerList );
    }

    //Such complex expression is not supported by linq it shows error of could not be translated 
    /*private Func<MissionApplication, long, bool> applicationFilter = 
        (application, missionId) =>
        {
            return (application.MissionId == missionId && application.ApprovalStatus == 1);
        };*/

    public static RecentVolunteersVM ConvertMissionApplicationToRecentVolunteerVM( MissionApplication application )
    {
        return new RecentVolunteersVM()
        {
            UserId = application.UserId,
            Avatar = application.User.Avatar ?? string.Empty,
            UserName = application.User.FirstName + " " + application.User.LastName
        };
    }
    
    //Fetches missions where users participated as volunteer
    public SingleUserMissionsVM GetSingleUserMission(long userId)
    {
        Func<MissionApplication, bool> filter = (application) => application.UserId == userId;
        var userMissionList = unitOfWork.MissionApplicationRepo.FetchSingleUserMissions( filter );

        SingleUserMissionsVM userMission = new();

        if (userMissionList.Any())
        {
            userMission.MissionId = userMissionList.Select(missions => missions.MissionId).ToList();
            userMission.MissionTitle = userMissionList.Select(missions => missions.Mission.Title!).ToList();
        }

        return userMission;
    }
 

    public IEnumerable<SingleMissionVM> LoadUserApprovedMissions(long id)
    {
        Func<MissionApplication, bool> filter = (application) => application.UserId == id && application.ApprovalStatus == 1;
        var missionList =
            unitOfWork.MissionApplicationRepo.FetchSingleUserMissions(filter);

        IEnumerable<SingleMissionVM> userMissionList = new List<SingleMissionVM>();
        if (!missionList.Any()) return userMissionList;

        userMissionList =
            missionList
                .Select( application => new SingleMissionVM()
                {
                    MissionId = application.MissionId,
                    Title = application.Mission.Title?? string.Empty,
                    MissionType = application.Mission.MissionType ? MissionTypeEnum.TIME : MissionTypeEnum.GOAL,
                    StartDate = application.Mission.StartDate,
                    EndDate = application.Mission.EndDate
                });
        return userMissionList;
    }

    public void DeleteMissionApplication(long missionId, long userId)
    {
        int result = unitOfWork.MissionApplicationRepo.DeleteMissionApplication(missionId, userId);
        if (result == 0) throw new Exception("Something went wrong during delete mission application !");
    }

    public void SaveApplication(long missionId, long userId)
    {
        MissionApplication application = new()
        {
            MissionId = missionId,
            UserId = userId,
            CreatedAt = DateTimeOffset.Now,
            AppliedAt = DateTimeOffset.Now,
            ApprovalStatus = 0
        };

        unitOfWork.MissionApplicationRepo.Add(application);
        unitOfWork.Save();
    }

    public IEnumerable<MissionApplicationVM> LoadApplications()
    {
        IEnumerable<MissionApplication> applications = unitOfWork.MissionApplicationRepo.LoadAllApplications();

        return
            applications
                .OrderByDescending(app => app.CreatedAt)
                .Select(ConvertToApplicationVM);
    }

    public void UpdateApplicationStatus(long id, byte status)
    {
        int result = unitOfWork.MissionApplicationRepo.UpdateApplicationStatus(id, status);
        if (result == 0) throw new Exception("Application status could not be changed!");
    }

    public IEnumerable<MissionApplicationVM> SearchApplication(string searchKey)
    {
        if (string.IsNullOrWhiteSpace(searchKey) || string.IsNullOrEmpty(searchKey))
            return LoadApplications();

        Func<MissionApplication, bool> filter = app => app.Mission.Title!.ContainsCaseInsensitive(searchKey) ||
                                                       app.User.FirstName.ContainsCaseInsensitive(searchKey) ||
                                                       app.User.LastName.ContainsCaseInsensitive(searchKey); 
        IEnumerable<MissionApplication> applications = unitOfWork.MissionApplicationRepo.LoadApplications(filter);

        return
            applications
                .Select(ConvertToApplicationVM);
    }

    public long UserMissionCount(long id, MissionTypeEnum type)
    {
        bool checkFor = type != MissionTypeEnum.GOAL;

        var applications = unitOfWork.MissionApplicationRepo.FetchApplicationWithMission();

        var count =
            applications
                .Where(app => (app.UserId == id && app.Mission.MissionType == checkFor && app.ApprovalStatus == 1))
                .LongCount();
        return count;
    }


    private static MissionApplicationVM ConvertToApplicationVM(MissionApplication app)
    {
        MissionApplicationVM vm = new()
        {
            MissionId = app.MissionId,
            UserId = app.UserId,
            ApplicationId = app.MissionApplicationId,
            ApprovalStatus = (ApprovalStatus) (app.ApprovalStatus ?? 0),
            AppliedAt = app.AppliedAt,
            MissionTitle = app.Mission.Title?? string.Empty,
            UserName = $"{app.User.FirstName} {app.User.LastName}"
        };
        return vm;
    }
}
