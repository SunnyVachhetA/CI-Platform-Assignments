using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class Notification
{
    public long NotificationId { get; set; }

    public string Message { get; set; } = null!;

    public byte NotificationType { get; set; }
}
