using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Services.Service.Interface;
public interface ITimesheetService
{
    List<VolunteerTimesheetVM> LoadUserTimesheet(long id);
    void SaveUserVolunteerHours(VolunteerHourVM volunteerHour);
    IEnumerable<VolunteerTimesheetVM> LoadUserTimesheet(long volunteerHourUserId, MissionTypeEnum goal);
}
