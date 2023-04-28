
using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class VolunteerHourVM
{
    public long TimesheetId { get; set; }
    public long UserId { get; set; }

    [Required(ErrorMessage = "Please select mission!")]
    [Display(Name="Mission")]
    public long MissionId { get; set; }

    [Required(ErrorMessage = "Please select volunteered date!")]
    [Display(Name = "Date Volunteered")]
    public DateTimeOffset? Date { get; set; }

    [Required(ErrorMessage = "Please enter volunteered hours!")]
    [Range(0, 23, ErrorMessage = "Hours must be between 0 to 23!")]
    public int? Hours { get; set; }

    [Required(ErrorMessage = "Please enter volunteered minutes!")]
    [Range(0, 59, ErrorMessage = "Minutes must be between 0 to 59!")]
    public int? Minutes { get; set; }

    [Required(ErrorMessage = "Please enter message regarding volunteer work!")]
    public string Message { get; set; }

    public IEnumerable<SingleMissionVM> MissionList { get; set; } = new List<SingleMissionVM>();
}
