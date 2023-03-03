using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class Mission
{
    public long MissionId { get; set; }

    public short? ThemeId { get; set; }

    public int? CityId { get; set; }

    public byte? CountryId { get; set; }

    public string? Title { get; set; }

    public string? ShortDescription { get; set; }

    public string? Desciription { get; set; }

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

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<MissionMedium> MissionMedia { get; } = new List<MissionMedium>();

    public virtual MissionTheme? Theme { get; set; }
}
