using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface ITimesheetService
{
    List<VolunteerTimesheetVM> LoadUserTimesheet(long id);
}
