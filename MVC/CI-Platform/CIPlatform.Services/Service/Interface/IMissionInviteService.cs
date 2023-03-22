using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IMissionInviteService
{
    IEnumerable<UserMissionInviteVM> fetchUserMissionInvites(IEnumerable<UserRegistrationVM> result, long userId, long missionId);
}
