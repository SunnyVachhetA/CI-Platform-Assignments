using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class City
{
    public int CityId { get; set; }

    public byte CountryId { get; set; }

    public string? Name { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public virtual ICollection<Mission> Missions { get; } = new List<Mission>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
