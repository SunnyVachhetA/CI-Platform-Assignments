using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class StoryInvite
{
    public long StoryInviteId { get; set; }

    public long StoryId { get; set; }

    public long FromUserId { get; set; }

    public long ToUserId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public virtual User FromUser { get; set; } = null!;

    public virtual Story Story { get; set; } = null!;

    public virtual User ToUser { get; set; } = null!;
}
