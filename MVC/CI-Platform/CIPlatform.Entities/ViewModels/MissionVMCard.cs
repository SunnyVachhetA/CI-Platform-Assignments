using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Entities.ViewModels;
public class MissionVMCard
{
    public long MissionId { get; set; }
    public short ThemeId { get; set; }
    public string ThemeName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty; 
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
    public byte? CountryId { get; set; }
    public string? Description { get; set; }

    public IEnumerable<long> FavoriteMissionList = new List<long>();
    public IEnumerable<long> ApprovedApplicationList = new List<long>();
    public IEnumerable<short> MissionSkill = new List<short>();
    public IEnumerable<long> ApplicationListId { get; set; } = new List<long>();

    public DateTimeOffset CreatedAt { get; set; }
}
