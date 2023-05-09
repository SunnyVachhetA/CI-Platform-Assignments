using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Services.Service.Interface;

public interface IPushNotificationService
{
    Task<List<UserContactVM>> PushNotificationToAllUsers(string message, NotificationTypeEnum notificationType, NotificationTypeMenu menu);
    Task PushEmailNotificationToSubscriberAsync(string title, string pageLink, List<UserContactVM> emailSubscriptionList, NotificationTypeMenu menu);
    Task<bool> PushNotificationToUserAsync(UserNotificationTemplate template);
    Task PushEmailNotificationToUserAsync(UserNotificationTemplate template, string link);
    Task PushRecommendNotificationToUsersAsync(string message, IEnumerable<UserNotificationSettingPreferenceVM> userPrefrence, string avatar, NotificationTypeEnum type, NotificationTypeMenu menu);
    Task PushRecommendEmailNotificationToUsersAsync(IEnumerable<UserNotificationSettingPreferenceVM> userPrefrence, string title, string link, string senderUserName, NotificationTypeMenu menu);
}
