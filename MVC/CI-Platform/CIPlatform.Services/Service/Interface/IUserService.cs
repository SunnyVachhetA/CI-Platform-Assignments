using CIPlatform.Entities.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service.Interface;
public interface IUserService
{
    long Add(UserRegistrationVM user);
    IEnumerable<UserRegistrationVM> FetchAllUsers(bool isActiveFlag);
    Task<string> GetUserName(long userId);
    bool IsEmailExists(string email);
    Task SendUserMissionInviteService(IEnumerable<string> userEmailList, string senderUserName, string missionInviteLink, IEmailService _emailService);
    Task<IEnumerable<UserRegistrationVM>> LoadAllActiveUserForRecommendMissionAsync(long userId);
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
    Task<UserRegistrationVM> LoadUserBasicInformationAsync(long userId);
    void SetUserStatusToActive(string email);
    bool CheckUserDetailsFilled(long userId);
    bool CheckIsEmailUnique(string email);
    Task<long> AddUserByAdmin(AdminUserInfoVM user, string rootPath, string link, string token);
    AdminUserInfoVM LoadUserProfileEdit(long id);
    Task UpdateUserByAdmin(AdminUserInfoVM user, string wwwRootPath);
    UserRegistrationVM CheckAdminCredential(UserLoginVM credential);
}
