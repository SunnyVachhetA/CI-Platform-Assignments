using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
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
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Password = encryptedPassword,
            Avatar = user.Avatar
        };
        _unitOfWork.UserRepo.Add(obj);
        _unitOfWork.Save();
    }

    public bool IsEmailExists(string email)
    {
        var result = _unitOfWork.UserRepo.GetFirstOrDefault(
                user => user.Email == email
            );
        Console.WriteLine("Email Exists: " + result!=null);
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
            Status = result.Status ?? false
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
            Avatar = result.Avatar!
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
        var subject = "Mission Invitation From Co-Worker";
        foreach( var email in userEmailList )
        {
            _emailService.EmailSend( email, subject, inviteMessage);
        }
    }

    private string CreateMissionInviteMessage( string senderUserName, string missionInviteLink )
    {
        string message = 
            @$"
                <div class='text-center'>
                <h2>You got mission invite from your co-worker { senderUserName }</h2>

                <h4>Check Out Mission Details By Clicking Below Button</h4>

                <hr>
                
                <a href = '{missionInviteLink}'> <button class='btn-g-orange'>Mission Information</button> </a>
                </div>
            ";

        return message;
    }
    
    public UserProfileVM LoadUserProfile(long id)
    {
        Func<User, bool> filter = user => ( user.UserId == id && (user.Status?? true) );
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

    public void UpdateUserAvatar(IFormFile file, string wwwRoot, long userId)
    {
        MediaVM media = StoreMediaService.storeMediaToWwwRoot( wwwRoot, @"images\user", file );

        _unitOfWork.UserRepo.UpdateUserAvatar( $"{media.Path}{media.Name}{media.Type}", userId );
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
}
