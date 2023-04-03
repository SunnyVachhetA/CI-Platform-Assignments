using System.ComponentModel.DataAnnotations;
using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Entities.ViewModels;
public class UserProfileVM
{
    public long UserId { get; set; }

    public string Avatar { get; set; } = string.Empty;

    [Required(ErrorMessage = "Name is required!")]
    [Display(Name="Name*")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Surname is required!")]
    [Display(Name="Surname*")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name="Employee ID")]
    public string? EmployeeId { get; set; }
    
    public string? Title { get; set; }

    public string? Department { get; set; }

    [Required(ErrorMessage = "My profile cannot be empty!")]
    [MinLength(15, ErrorMessage = "At least 15 characters required!")]
    [Display(Name="My Profile*")]
    public string MyProfile { get; set; } = string.Empty;

    [Display(Name="Why I Volunteer?")]
    public string? WhyIVolunteer { get; set; }

    [Required(ErrorMessage = "Please select country!")]
    [Display(Name="Country*")]
    public byte CountryId { get; set; }

    [Required(ErrorMessage = "Please select city!")]
    [Display(Name = "City*")]
    public int CityId { get; set; }

    public MissionAvailability? Availability { get; set; }

    [Display(Name="LinkedIn")]
    public string? LinkedInUrl { get; set; }

    public List<SkillVM> AllSkills { get; set; } = new();

    public List<UserSkillVM> UserSkills { get; set; } = new();

    public List<CountryVM> CountryList { get; set; } = new();
    public List<CityVM> CityList { get; set; } = new();
}
