using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class CmsPage
{
    public short CmsPageId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Slug { get; set; } = null!;

    public bool? Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
