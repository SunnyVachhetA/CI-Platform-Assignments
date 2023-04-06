using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class VolunteerGoalVM
{
    public long TimesheetId { get; set; }
    public long UserId { get; set; }

    [Required(ErrorMessage = "Please select mission!")]
    [Display(Name = "Mission")]
    public long MissionId { get; set; }

    [Required(ErrorMessage = "Please select volunteered date!")]
    [Display(Name = "Date Volunteered")]
    public DateTimeOffset Date { get; set; }

    [Required(ErrorMessage = "Please enter action!")]
    public long Action { get; set; }

    [Required(ErrorMessage = "Please enter message regarding volunteer work!")]
    public string Message { get; set; }

    public IEnumerable<SingleMissionVM> MissionList { get; set; } = new List<SingleMissionVM>();
}
