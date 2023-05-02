using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class UserNotification
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public byte TypeId { get; set; }

    public string? Message { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public bool? IsRead { get; set; }

    public virtual NotificationType Type { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
