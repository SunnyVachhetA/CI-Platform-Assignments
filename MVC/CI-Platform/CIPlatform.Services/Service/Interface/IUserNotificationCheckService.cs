namespace CIPlatform.Services.Service.Interface;

public interface IUserNotificationCheckService
{
    Task UpdateLastCheckAsync(long userId);
}
