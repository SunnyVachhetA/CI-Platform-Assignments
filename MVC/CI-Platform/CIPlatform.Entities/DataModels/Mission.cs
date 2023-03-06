using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class Mission
{
    public long MissionId { get; set; }

    public short? ThemeId { get; set; }

    public byte? CountryId { get; set; }

    public string? Title { get; set; }

    public string? ShortDescription { get; set; }

    public DateTimeOffset? StartDate { get; set; }

    public DateTimeOffset? EndDate { get; set; }

    public bool MissionType { get; set; }

    public bool? Status { get; set; }

    public string? OrganizationName { get; set; }

    public string? OrganizationDetail { get; set; }

    public byte? Availability { get; set; }

    public long? TotalSeat { get; set; }

    public DateTimeOffset? RegistrationDeadline { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public string? Description { get; set; }

    public byte? Rating { get; set; }

    public int? CityId { get; set; }

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<FavouriteMission> FavouriteMissions { get; } = new List<FavouriteMission>();

    public virtual ICollection<GoalMission> GoalMissions { get; } = new List<GoalMission>();

    public virtual ICollection<MissionApplication> MissionApplications { get; } = new List<MissionApplication>();

    public virtual ICollection<MissionMedium> MissionMedia { get; } = new List<MissionMedium>();

    public virtual ICollection<MissionSkill> MissionSkills { get; } = new List<MissionSkill>();

    public virtual MissionTheme? Theme { get; set; }
}
