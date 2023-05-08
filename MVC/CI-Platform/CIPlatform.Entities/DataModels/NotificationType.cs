using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class NotificationType
{
    public byte TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();
}
