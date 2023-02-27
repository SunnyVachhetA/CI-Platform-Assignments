﻿using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class Banner
{
    public long BannerId { get; set; }

    public byte[] Image { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Text { get; set; }

    public int? SortOrder { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
