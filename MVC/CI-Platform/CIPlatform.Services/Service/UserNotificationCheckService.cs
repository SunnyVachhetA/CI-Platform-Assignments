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

    public async Task UpdateLastCheckAsync(long userId)
    {
        int result = await _unitOfWork.UserNotificationCheckRepo.UpdateUserLastCheckAsync(userId);
        if (result == 0) throw new Exception("Something went wrong during user last check: " + nameof(userId));
    }
}
