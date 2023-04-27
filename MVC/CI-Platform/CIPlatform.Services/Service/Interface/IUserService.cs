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
    string UpdateUserAvatar(IFormFile file, string wwwRoot, long userId);
    void UpdateUserDetails(UserProfileVM userProfile);

    IEnumerable<UserRegistrationVM> GetSortedUserList( bool isActiveFlag = false);
    int UpdateUserStatus(long userId, byte status);
    IEnumerable<UserRegistrationVM> FilterUserBySearchKey(string? key);
    void GenerateEmailVerificationToken(UserRegistrationVM user, string href, IEmailService emailService);
    void SetUserStatusToActive(string email);
    bool CheckUserDetailsFilled(long userId);
    bool CheckIsEmailUnique(string email);
    Task AddUserByAdmin(AdminUserInfoVM user, string rootPath, string link, string token);
    AdminUserInfoVM LoadUserProfileEdit(long id);
    Task UpdateUserByAdmin(AdminUserInfoVM user, string wwwRootPath);
    UserRegistrationVM CheckAdminCredential(UserLoginVM credential);
}
