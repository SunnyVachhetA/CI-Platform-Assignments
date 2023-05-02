using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class NotificationType
{
    public byte TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<UserNotification> UserNotifications { get; } = new List<UserNotification>();
}
