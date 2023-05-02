using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class UserNotificationCheck
{
    public long UserId { get; set; }

    public DateTimeOffset? LastCheck { get; set; }

    public virtual User User { get; set; } = null!;
}
