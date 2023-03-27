using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

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
}
