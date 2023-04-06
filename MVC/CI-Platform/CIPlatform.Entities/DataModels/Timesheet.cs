using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class Timesheet
{
    public long TimesheetId { get; set; }

    public long UserId { get; set; }

    public long MissionId { get; set; }

    public TimeSpan? Time { get; set; }

    public int? Action { get; set; }

    public DateTimeOffset? DateVolunteered { get; set; }

    public string? Notes { get; set; }

    public byte? Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public virtual Mission Mission { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
