using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IMissionInviteService
{
    IEnumerable<UserInviteVm> fetchUserMissionInvites(IEnumerable<UserRegistrationVM> result, long userId, long missionId);
    Task<IEnumerable<string>> SaveMissionInviteFromUser(long userId, long missionId, long[] userList);
}
