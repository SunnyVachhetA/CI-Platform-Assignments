
using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Entities.ViewModels;

public class UserNotificationVM
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public NotificationTypeEnum NotificationType { get; set; }

    public string? FromUserAvatar { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }
}
