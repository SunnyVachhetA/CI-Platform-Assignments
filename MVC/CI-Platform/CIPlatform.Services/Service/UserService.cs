using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

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
}
