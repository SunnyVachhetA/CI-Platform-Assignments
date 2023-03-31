using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IStoryInviteService
{
    IEnumerable<UserInviteVm> LoadAllUsersInviteList(long userId, long storyId);
    Task SaveUserInvite(long userId, long storyId, long[] recommendList);
    Task SendStoryInviteToUsers(IEnumerable<string> userEmailList, string storyInviteLink, string userName);
}
