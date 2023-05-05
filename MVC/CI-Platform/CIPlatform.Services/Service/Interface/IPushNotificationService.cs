using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Services.Service.Interface;

public interface IPushNotificationService
{
    Task<List<UserContactVM>> PushNotificationToAllUsers(string message, NotificationTypeEnum notificationType, NotificationTypeMenu menu);
    Task PushEmailNotificationToSubscriberAsync(string title, string pageLink, List<UserContactVM> emailSubscriptionList, NotificationTypeMenu menu);
}
