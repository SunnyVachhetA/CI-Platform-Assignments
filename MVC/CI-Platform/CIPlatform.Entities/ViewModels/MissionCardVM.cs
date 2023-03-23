using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Entities.ViewModels;
public class MissionCardVM
{
    public long MissionId { get; set; }
    public short? ThemeId { get; set; }
    public string? ThemeName { get; set; }
    public string? Title { get; set; }
    public string? ShortDescription { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public MissionTypeEnum MissionType { get; set; }
    public MissionStatus Status { get; set; }
    public string? OrganizationName { get; set; }
    public long? TotalSeat { get; set; }
    public long? SeatLeft { get; set; }
    public DateTimeOffset? RegistrationDeadline { get; set; }
    public byte? Rating { get; set; }
    public int? CityId { get; set; }
    public string? CityName { get; set; }
    public string? ThumbnailUrl { get; set; }
    public long? GoalValue { get; set; }
    public string? GoalText { get; set; }
    public long? GoalAchieved { get; set; }
    public long? NumberOfVolunteer { get; set; }
    public List<string>? MissionMedias { get; set; }
    public byte? CountryId { get; set; }
    public string? Description { get; set; }
    public string? OrganizationDetails { get; set; }
    public List<short>? SkillId { get; set; } = new();
    public List<string>? Skills { get; set; } = new();
    public List<long>? FavrouriteMissionsId { get; set; } = new();
    public List<long> ApplicationListId { get; set; } = new();
    public List<MissionRatingVM> MissionRating { get; set; } = new();
    public MissionAvailability? MissionAvailability { get; set; }
    public List<long>? CommentList { get; set; } = new();

    public List<RecentVolunteersVM> RecentVolunteers { get; set; } = new();

    public List<MissionDocumentVM> MissionDocuments { get; set; } = new();
}
