using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface ITimesheetRepository: IRepository<Timesheet>
{
    IEnumerable<Timesheet> GetUserTimesheet(Func<Timesheet, bool> filter);
    void DeleteTimesheetEntry(long timesheetId);
    IEnumerable<Timesheet> LoadTimesheet(Func<Timesheet, bool> filter);
    int UpdateTimesheetApprovalStatus(long timesheetId, byte status);
    Timesheet? FetchTimesheetEntry(Func<Timesheet, bool> filter);
}
