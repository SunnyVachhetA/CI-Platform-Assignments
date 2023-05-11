using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;

public class UserNotificationCheckService : IUserNotificationCheckService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserNotificationCheckService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void CreateUserLastCheck(long userId)
    {
        UserNotificationCheck check = new()
        {
            UserId = userId,    
            LastCheck = DateTimeOffset.Now,
        };

        _unitOfWork.UserNotificationCheckRepo.Add(check);
        _unitOfWork.Save();
    }

    public async Task UpdateLastCheckAsync(long userId)
    {
        int result = await _unitOfWork.UserNotificationCheckRepo.UpdateUserLastCheckAsync(userId);
        if (result == 0) throw new Exception("Something went wrong during user last check: " + nameof(userId));
    }
}
