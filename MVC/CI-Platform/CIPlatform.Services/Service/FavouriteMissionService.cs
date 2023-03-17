using CIPlatform.DataAccessLayer.Repository;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class FavouriteMissionService : IFavouriteMissionService
{
    private IUnitOfWork unitOfWork;

    public FavouriteMissionService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    //Adding Mission To User favourite
    public Task AddMissionToUserFavourite(long userId, long missionId)
    {
        unitOfWork.FavouriteMissionRepo.Add
            (
                new()
                {
                    UserId = userId,
                    MissionId = missionId,
                    CreatedAt = DateTimeOffset.Now
                }
            );
        unitOfWork.Save();
        return Task.CompletedTask;
    }

    public Task RemoveMissionFromUserFavrouite(long userId, long missionId)
    {
        
        var favMission = unitOfWork.FavouriteMissionRepo.GetFirstOrDefault( favMission => (favMission.UserId == userId && favMission.MissionId == missionId ) );
        
        if( favMission != null )
            unitOfWork.FavouriteMissionRepo.Remove( favMission );

        return Task.CompletedTask;
    }
}
