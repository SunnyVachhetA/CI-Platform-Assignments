namespace CIPlatform.Entities.ViewModels;

public class NotificationContainerVM
{
    public IEnumerable<UserNotificationVM> NewNotifications { get; set; } = new List<UserNotificationVM>();
    public IEnumerable<UserNotificationVM> OldNotifications { get; set; } = new List<UserNotificationVM>();

    public NotificationSettingVM NotificationSetting { get; set; } = new();
    public DateTimeOffset LastCheck { get; set; }

    public int UnreadCount { get; set; }
}
