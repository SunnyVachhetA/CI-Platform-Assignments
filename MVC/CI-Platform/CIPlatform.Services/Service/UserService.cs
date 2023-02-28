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
    public void Add(UserVM user)
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
        Console.WriteLine(user);

        Console.WriteLine(obj.UserId);
        _unitOfWork.UserRepo.Add(obj);
        _unitOfWork.Save();
    }

    public bool IsEmailExists(string email)
    {
        /*bool result = _unitOfWork.UserRepo.GetFirstOrDefaultEmail(email);*/
        return false;
    }
}
