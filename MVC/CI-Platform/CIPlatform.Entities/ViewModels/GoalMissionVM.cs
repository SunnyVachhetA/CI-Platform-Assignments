using CIPlatform.Entities.Validation;
using CIPlatform.Entities.VMConstants;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class GoalMissionVM
{
    public long MissionId { get; set; }

    [Required]
    [MinLength(15, ErrorMessage = "Title should have at least 15 characters.")]
    [RegularExpression(@"^\S+(\s*\S+)*$", ErrorMessage = "Title must not contain only whitespace characters.")]

    public string Title { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Short Description")]
    [RegularExpression(@"^\S+(\s*\S+)*$", ErrorMessage = "Short description must not contain only whitespace characters.")]
    public string ShortDescription { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Display(Name = "Organization Name")]
    [Required]
    public string OrganizationName { get; set; } = string.Empty;

    [Display(Name = "Organization Details")]
    public string? OrganizationDetail { get; set; }

    [Required]
    [Display(Name = "Start Date")]
    public DateTimeOffset? StartDate { get; set; }
    
    [Display(Name = "End Date")]
    public DateTimeOffset? EndDate { get; set; }

    [Display(Name = "Registration Deadline")]
    public DateTimeOffset? RegistrationDeadline { get; set; }

    [Display(Name = "Total Seats")]
    public long? TotalSeats { get; set; }

    [Display(Name = "City")]
    [Required(ErrorMessage = "Please select mission city.")]
    public int CityId { get; set; }

    [Display(Name = "Country")]
    [Required(ErrorMessage = "Please select mission country.")]
    public byte CountryId { get; set; }

    [Display(Name = "Mission Availability")]
    [Required]
    public MissionAvailability Availability { get; set; }

    [Required][Display(Name = "Status")] public bool? IsActive { get; set; }

    [Display(Name = "Mission Theme")]
    [Required(ErrorMessage = "Please select mission theme.")]
    public short? ThemeId { get; set; }

    [Display(Name = "Goal Value")]
    [Required]
    [Range(1, int.MaxValue)]
    public int? GoalValue { get; set; }
    
    [Display(Name = "Goal Objective")]
    [Required]
    public string GoalObjective { get; set; } = string.Empty;

    [Display(Name = "Select Skills")]
    [Required(ErrorMessage = "Please select at least one skill.")]
    public IEnumerable<short> Skills { get; set; } = new List<short>();

    [Required(ErrorMessage = "Please upload mission photos.")]
    public IEnumerable<IFormFile> Images { get; set; }

    public IEnumerable<IFormFile>? Documents { get; set; } = new List<IFormFile>();

    public IEnumerable<CityVM> CityList { get; set; } = new List<CityVM>();
    public IEnumerable<CountryVM> CountryList { get; set; } = new List<CountryVM>();
    public IEnumerable<SkillVM> SkillList { get; set; } = new List<SkillVM>();
    public IEnumerable<ThemeVM> ThemeList { get; set; } = new List<ThemeVM>();

    public IEnumerable<MediaVM> MediaList { get; set; } = new List<MediaVM>();
    public IEnumerable<MissionDocumentVM> DocumentList { get; set; } = new List<MissionDocumentVM>();
}
