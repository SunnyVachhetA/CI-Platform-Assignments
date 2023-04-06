using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface ITimesheetRepository: IRepository<Timesheet>
{
    IEnumerable<Timesheet> GetUserTimesheet(Func<Timesheet, bool> filter);
}
