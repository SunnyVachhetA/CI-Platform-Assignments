using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;

public interface INotificationSettingService
{
    Task UpdateNotificationSettingsAsync(NotificationSettingVM vm);
}
