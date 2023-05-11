namespace CIPlatform.Services.Service.Interface;

public interface IUserNotificationCheckService
{
    Task UpdateLastCheckAsync(long userId);
    void CreateUserLastCheck(long userId);
}
