﻿using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class MissionInvite
{
    public long MissionInviteId { get; set; }

    public long MissionId { get; set; }

    public long FromUserId { get; set; }

    public long ToUserId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public virtual User FromUser { get; set; } = null!;

    public virtual Mission Mission { get; set; } = null!;

    public virtual User ToUser { get; set; } = null!;
}
