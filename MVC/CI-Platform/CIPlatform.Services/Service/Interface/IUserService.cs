using CIPlatform.Entities.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service.Interface;
public interface IUserService
{
    void Add(UserRegistrationVM user);
    IEnumerable<UserRegistrationVM> FetchAllUsers(bool isActiveFlag);
    Task<string> GetUserName(long userId);
    bool IsEmailExists(string email);
    void SendUserMissionInviteService(IEnumerable<string> userEmailList, string senderUserName, string missionInviteLink, IEmailService _emailService);
    UserRegistrationVM UpdateUserPassword(string? email, string password);
    UserRegistrationVM ValidateUserCredential(UserLoginVM credential);
    Task<IEnumerable<string>> GetUserEmailList(long[] userId);
    UserProfileVM LoadUserProfile(long id);
    bool CheckOldCredentialAndUpdate(ChangePasswordVM passwordVm);
    void UpdateUserAvatar(IFormFile file, string wwwRoot, long userId);
    void UpdateUserDetails(UserProfileVM userProfile);

    IEnumerable<UserRegistrationVM> GetSortedUserList( bool isActiveFlag = false);
    int UpdateUserStatus(long userId, byte status);
    IEnumerable<UserRegistrationVM> FilterUserBySearchKey(string? key);
    void GenerateEmailVerificationToken(UserRegistrationVM user, string href, IEmailService emailService);
    void SetUserStatusToActive(string email);
}
