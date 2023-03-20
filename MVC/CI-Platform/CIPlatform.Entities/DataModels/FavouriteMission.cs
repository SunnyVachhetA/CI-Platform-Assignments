using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class FavouriteMission
{
    public long FavouriteMissionId { get; set; }

    public long MissionId { get; set; }

    public long UserId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public virtual Mission Mission { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
