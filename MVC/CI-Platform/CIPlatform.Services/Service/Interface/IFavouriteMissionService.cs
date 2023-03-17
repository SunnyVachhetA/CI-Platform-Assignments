namespace CIPlatform.Services.Service.Interface;
public interface IFavouriteMissionService
{
    Task AddMissionToUserFavourite(long userId, long missionId);
    Task RemoveMissionFromUserFavrouite(long userId, long missionId);
}
