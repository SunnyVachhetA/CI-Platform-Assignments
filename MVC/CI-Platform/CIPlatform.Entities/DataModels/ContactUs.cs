
namespace CIPlatform.Entities.DataModels;

public partial class ContactUs
{
    public long ContactId { get; set; }

    public long UserId { get; set; }

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string Response { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public byte? Status { get; set; }
    
    public DateTimeOffset? DeletedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual User User { get; set; } = null!;
}
