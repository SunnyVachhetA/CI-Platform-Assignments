using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class UserService: IUserService
{
    private readonly IUnitOfWork _unitOfWork;
	public UserService( IUnitOfWork unitOfWork )
	{
		_unitOfWork = unitOfWork;
	}
    public void Add(UserRegistrationVM user)
    {
        Console.WriteLine("Converting VM = DM");
        User obj = new User()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Password = user.Password
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
        
        _unitOfWork.UserRepo.UpdatePassword(email, password);

        var result = _unitOfWork.UserRepo.GetFirstOrDefault(
                user => user.Email == email
            );

        var user = ConvertToRegistrationVM(result);

        return user;
    }

    public UserRegistrationVM ValidateUserCredential(UserLoginVM credential)
    {
        User? result = _unitOfWork.UserRepo.ValidateUserCredentialRepo( credential );

        if (result == null) return null;

        UserRegistrationVM user = new UserRegistrationVM()
        {
            UserId =  result.UserId,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            PhoneNumber = result.PhoneNumber,
            Password = result.Password,
            Avatar = result.Avatar
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
}
