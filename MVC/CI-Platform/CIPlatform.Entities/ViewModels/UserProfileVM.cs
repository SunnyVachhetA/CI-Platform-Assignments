using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Entities.ViewModels;
public class UserProfileVM
{
    public long UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Avatar { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public long EmployeeId { get; set; }

    public string? Title { get; set; }

    public string? Department { get; set; }

    public string MyProfile { get; set; } = string.Empty;

    public string? WhyIVolunteer { get; set; }

    public byte CountryId { get; set; }
    public int CityId { get; set; }

    public MissionAvailability? Availability { get; set; }

    public string? LinkedInUrl { get; set; }

    public List<string> Skills { get; set; } 
}
