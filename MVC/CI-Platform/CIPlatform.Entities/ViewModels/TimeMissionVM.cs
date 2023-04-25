using System.ComponentModel.DataAnnotations;
using CIPlatform.Entities.Validation;
using CIPlatform.Entities.VMConstants;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Entities.ViewModels;
public class TimeMissionVM
{
    public long MissionId { get; set; }

    [Required(ErrorMessage = "Please enter mission title!")]
    [MinLength(15, ErrorMessage = "Title should have at least 15 characters!")]
    [NoWhiteSpace(ErrorMessage = "Title cannot be empty or contain only whitespace.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter mission short description!")]
    [MinLength(20, ErrorMessage = "Short Description should have at least 15 characters!")]
    [NoWhiteSpace(ErrorMessage = "Title field cannot be empty or contain only whitespace.")]
    [Display(Name="Short Description")]
    public string ShortDescription { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Display(Name = "Organization Name")]
    [Required]
    public string OrganizationName { get; set; } = string.Empty;

    [Display(Name="Organization Details")]
    public string? OrganizationDetail { get; set; }

    [Required]
    [Display(Name="Start Date")]
    public DateTimeOffset? StartDate { get; set; }

    [Required]
    [Display(Name = "End Date")]
    public DateTimeOffset? EndDate { get; set; }

    [Display(Name = "Registration Deadline")]
    public DateTimeOffset? RegistrationDeadline { get; set; }

    [Display(Name="Total Seats")]
    public long? TotalSeats { get; set; }

    [Display(Name="City")]
    [Required(ErrorMessage = "Please select mission city!")]
    public int CityId { get; set; }

    [Display(Name="Country")]
    [Required(ErrorMessage = "Please select mission country!")]
    public byte CountryId { get; set; }

    [Display(Name="Mission Availability")]
    [Required]
    public MissionAvailability Availability { get; set; }

    [Required] [Display(Name = "Status")] public bool? IsActive { get; set; }

    [Display(Name="Mission Theme")]
    [Required(ErrorMessage = "Please select mission theme!")]
    public short? ThemeId { get; set; }

    [Display(Name = "Select Skills")] 
    [Required(ErrorMessage = "Please select at least one skill!")]
    public IEnumerable<short> Skills { get; set; } = new List<short>();

    [Required(ErrorMessage = "Please upload mission photos!")]
    public IEnumerable<IFormFile> Images { get; set; }

    public IEnumerable<IFormFile>? Documents { get; set; } = new List<IFormFile>();

    public IEnumerable<CityVM> CityList { get; set; } = new List<CityVM>();
    public IEnumerable<CountryVM> CountryList { get; set; } = new List<CountryVM>();
    public IEnumerable<SkillVM> SkillList { get; set; } = new List<SkillVM>();
    public IEnumerable<ThemeVM> ThemeList { get; set; } = new List<ThemeVM>();

    public IEnumerable<MediaVM> MediaList { get; set; } = new List<MediaVM>();
    public IEnumerable<MissionDocumentVM> DocumentList { get; set; } = new List<MissionDocumentVM>();
}
