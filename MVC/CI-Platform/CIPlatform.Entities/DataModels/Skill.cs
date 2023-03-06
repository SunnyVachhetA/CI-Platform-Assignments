using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class Skill
{
    public short SkillId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public virtual ICollection<MissionSkill> MissionSkills { get; } = new List<MissionSkill>();
}
