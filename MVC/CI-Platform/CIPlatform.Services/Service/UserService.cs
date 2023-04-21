using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service;
public class UserService: IUserService
{
    private readonly IUnitOfWork _unitOfWork;

	public UserService( IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

    public void Add(UserRegistrationVM user)
    {
        string encryptedPassword = EncryptionService.EncryptAES(user.Password);
        User obj = new User()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email.ToLower(),
            PhoneNumber = user.PhoneNumber,
            Password = encryptedPassword,
            Avatar = user.Avatar,
            Status = false
        };
        _unitOfWork.UserRepo.Add(obj);
        _unitOfWork.Save();
    }
    public bool IsEmailExists(string email)
    {
        var result = _unitOfWork.UserRepo.GetFirstOrDefault(
                user => user.Email == email
            );
        return (result != null);
    }

    
    public static UserRegistrationVM ConvertToRegistrationVM(User result)
    {
        UserRegistrationVM user = new()
        {
            UserId = result.UserId,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            PhoneNumber = result.PhoneNumber,
            Avatar = result.Avatar,
            Status = result.Status ?? false,
            EmployeeID = result.EmployeeId,
            Department = result.Department
        };
        return user;
    }
    public UserRegistrationVM UpdateUserPassword(string? email, string password)
    {
        
        password = EncryptionService.EncryptAES(password);
        _unitOfWork.UserRepo.UpdatePassword(email, password);

        var result = _unitOfWork.UserRepo.GetFirstOrDefault(
                user => user.Email == email
            );

        var user = ConvertToRegistrationVM(result);

        return user;
    }

    public UserRegistrationVM ValidateUserCredential(UserLoginVM credential)
    {
        credential.Password = DecryptionService.DecryptAES(credential.Password);
        User? result = _unitOfWork.UserRepo.ValidateUserCredentialRepo( credential );

        if (result == null) return null!;

        UserRegistrationVM user = new UserRegistrationVM()
        {
            UserId =  result.UserId,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            PhoneNumber = result.PhoneNumber,
            Password = result.Password,
            Avatar = result.Avatar!,
            Status = result.Status?? false
        };

        return user;
    }

    public async Task<IEnumerable<string>> GetUserEmailList(long[] userId)
    {
        Func<User, bool> filter = user => userId.Any(id => user.UserId == id);
        IEnumerable<string> userEmailList =
            await 
                _unitOfWork
                .UserRepo
                .GetUserEmailList( filter );
        return userEmailList;
    }


    //Method to get all users
    // isActive = True => Fetch only active users otherwise all
    public IEnumerable<UserRegistrationVM> FetchAllUsers(bool isActiveFlag)
    {
        IEnumerable<User> result;
        IEnumerable<UserRegistrationVM> users = Enumerable.Empty<UserRegistrationVM>();

        if(isActiveFlag)
        {
            result = _unitOfWork.UserRepo.GetAll(ActiveUserFilter);
        }
        else
        {
            result = _unitOfWork.UserRepo.GetAll();
        }
        if(result.Any())
        {
            users = result.Select( user => ConvertToRegistrationVM(user) );
        }
        return users;
    }

    private Func<User, bool> ActiveUserFilter = (user) => user.Status ?? false;


    public Task<string> GetUserName(long userId)
    {
        var user = _unitOfWork.UserRepo.GetFirstOrDefault( user => user.UserId == userId );

        return Task.Run( () => user.FirstName + " " + user.LastName ); 
    }

    public async void SendUserMissionInviteService(IEnumerable<string> userEmailList, string senderUserName, string missionInviteLink, IEmailService _emailService)
    {
        var inviteMessage = CreateMissionInviteMessage( senderUserName, missionInviteLink );
        var subject = "Mission Invitation From Co-Worker | CI Platform";
        foreach( var email in userEmailList )
        {
            _emailService.EmailSend( email, subject, inviteMessage);
        }
    }

    private string CreateMissionInviteMessage(string senderUserName, string missionInviteLink) =>
        MailMessageFormatUtility.GenerateMissionInviteMessage(senderUserName, missionInviteLink);
    
    
    public UserProfileVM LoadUserProfile(long id)
    {
        Func<User, bool> filter = user => user.UserId == id ;
        User user = _unitOfWork.UserRepo.FetchUserProfile( filter );
        if (user == null) return null!;

        UserProfileVM userProfile = ConvertUserToUserProfileVM( user );
        return userProfile;
    }

    public bool CheckOldCredentialAndUpdate(ChangePasswordVM passwordVm)
    {
        string encodedPassword = EncryptionService.EncryptAES( passwordVm.OldPassword );

        User user =
            _unitOfWork
                .UserRepo
                .GetFirstOrDefault(user => (user.UserId == passwordVm.UserId && user.Password == encodedPassword));

        if (user == null) return false;

        user.Password = EncryptionService.EncryptAES( passwordVm.NewPassword );

        _unitOfWork.Save();
        return true;
    }

    public string UpdateUserAvatar(IFormFile file, string wwwRoot, long userId)
    {
        MediaVM media = StoreMediaService.storeMediaToWwwRoot( wwwRoot, @"images\user", file );

        _unitOfWork.UserRepo.UpdateUserAvatar( $"{media.Path}{media.Name}{media.Type}", userId );

        return $"{media.Path}{media.Name}{media.Type}";
    }

    public void UpdateUserDetails(UserProfileVM userProfile)
    {
        var user = _unitOfWork.UserRepo.GetFirstOrDefault( user => user.UserId == userProfile.UserId );

        if (user == null) return;

        user.FirstName = userProfile.FirstName;
        user.LastName = userProfile.LastName;
        user.Department = userProfile.Department;
        user.EmployeeId = userProfile.EmployeeId;
        user.Title = userProfile.Title;
        user.ProfileText = userProfile.MyProfile;
        user.WhyIVolunteer = userProfile.WhyIVolunteer;
        user.CityId = userProfile.CityId;
        user.CountryId = userProfile.CountryId;
        user.Availability = (byte?)userProfile.Availability;
        user.LinkedInUrl = userProfile.LinkedInUrl;
        user.UpdatedAt = DateTimeOffset.Now;
        user.PhoneNumber = userProfile.PhoneNumber?? user.PhoneNumber;

        _unitOfWork.Save();
    }

    public IEnumerable<UserRegistrationVM> GetSortedUserList(bool isActiveFlag = false)
    {
        IEnumerable<UserRegistrationVM> allUsers = FetchAllUsers(isActiveFlag);
        allUsers = allUsers.OrderByDescending(user => user.Status).ThenBy(user => user.FirstName);
        return allUsers;
    }

    public int UpdateUserStatus(long userId, byte status) =>
        _unitOfWork.UserRepo.UpdateUserStatus(userId, status);

    public IEnumerable<UserRegistrationVM> FilterUserBySearchKey(string? key)
    {

        if (string.IsNullOrEmpty(key)) return _unitOfWork.UserRepo.GetAll().Select(ConvertToRegistrationVM);

        Func<User, bool> filter = user => user.FirstName.ContainsCaseInsensitive(key) || user.LastName.ContainsCaseInsensitive(key);

        var users = _unitOfWork.UserRepo.GetAll(filter);

        var filteredUsersList = users.Select( ConvertToRegistrationVM );
        return filteredUsersList;
    }
    
    private UserProfileVM ConvertUserToUserProfileVM(User user)
    {
        UserProfileVM userProfileVm = new()
        {
            UserId = user.UserId,
            Avatar = user.Avatar?? string.Empty,
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmployeeId = user.EmployeeId,
            Title = user.Title,
            Department = user.Department,
            MyProfile = user.ProfileText?? string.Empty,
            WhyIVolunteer = user.WhyIVolunteer,
            CountryId = user.CountryId?? 0,
            CityId = user.CityId?? 0,
            Availability = (MissionAvailability) (user.Availability?? 0),
            LinkedInUrl = user.LinkedInUrl,
            UserSkills = ConvertToUserSkill(user.UserSkills),
            PhoneNumber = user.PhoneNumber
        };
        return userProfileVm;
    }

    private List<UserSkillVM> ConvertToUserSkill(ICollection<UserSkill> userSkills)
    {
        List<UserSkillVM> userSkillList = new();

        if (!userSkills.Any()) return userSkillList;

        userSkillList =
            userSkills
                .Select
                (
                    skill => new UserSkillVM()
                    {
                        UserSkillId = skill.UserSkillId,
                        UserId = skill.UserId,
                        SkillId = skill.SkillId,
                        Name = skill.Skill.Name
                    }
                ).ToList();

        return userSkillList;
    }
    public void GenerateEmailVerificationToken(UserRegistrationVM user, string href, IEmailService emailService)
    {
        string message = MailMessageFormatUtility.GenerateMessageForAccountActivation(user.FirstName, href);
        string subject = "Account Activation Email - CI Platform";
        
        emailService.EmailSend(user.Email, subject, message);
    }

    public void SetUserStatusToActive(string email)
    {
        _unitOfWork.UserRepo.SetUserStatusToActive(email);
    }

    public bool CheckUserDetailsFilled(long userId)
    {
        var user = _unitOfWork.UserRepo.GetFirstOrDefault(user => user.UserId == userId);
        if (user == null) throw new Exception("User not found with ID: " + userId);

        return user.CityId.HasValue;
    }
}
