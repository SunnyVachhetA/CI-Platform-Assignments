using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;

public class UserNotificationCheckService : IUserNotificationCheckService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserNotificationCheckService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}
