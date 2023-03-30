using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IStoryInviteService
{
    IEnumerable<UserInviteVm> LoadAllUsersInviteList(long userId, long storyId);
}
