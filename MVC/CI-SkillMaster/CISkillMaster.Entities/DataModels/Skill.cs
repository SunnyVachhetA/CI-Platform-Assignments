using System;
using System.Collections.Generic;

namespace CISkillMaster.Entities.DataModels;

public partial class Skill
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public bool? Status { get; set; }
}
