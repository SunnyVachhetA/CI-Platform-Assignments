namespace CIPlatform.Entities.ViewModels;
public class MissionRatingVM
{ 
    public long? UserId { get; set; }
    public byte? Rating { get; set; }
    public long MissionId { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
}
