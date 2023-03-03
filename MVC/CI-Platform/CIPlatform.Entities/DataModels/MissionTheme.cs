using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class MissionTheme
{
    public short ThemeId { get; set; }

    public string? Title { get; set; }

    public bool? Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public virtual ICollection<Mission> Missions { get; } = new List<Mission>();
}
