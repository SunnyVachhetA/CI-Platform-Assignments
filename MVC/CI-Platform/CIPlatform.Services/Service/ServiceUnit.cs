
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class ServiceUnit: IServiceUnit
{
    private readonly IUnitOfWork _unitOfWork;

	public ServiceUnit(IUnitOfWork unitOfWork)
	{
		_unitOfWork= unitOfWork;
		UserService = new UserService(_unitOfWork);
		PasswordResetService = new PasswordResetService(_unitOfWork);
	}

    public IUserService UserService { get; private set; }

    public IPasswordResetService PasswordResetService { get; private set; }
}
