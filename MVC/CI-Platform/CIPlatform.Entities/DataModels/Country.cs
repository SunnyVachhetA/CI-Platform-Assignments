using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class Country
{
    public byte CountryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Iso { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
