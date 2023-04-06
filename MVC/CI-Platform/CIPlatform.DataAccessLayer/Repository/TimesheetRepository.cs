using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class TimesheetRepository: Repository<Timesheet>, ITimesheetRepository
{
    public TimesheetRepository(CIDbContext dbContext) : base(dbContext)
    {
    }

    private IQueryable<Timesheet> TimesheetWithUserAndMission()
    {
        var query = 
            dbSet
            .Include(timesheet => timesheet.User)
            .Include(timesheet => timesheet.Mission);
        return query;
    }

    public IEnumerable<Timesheet> GetUserTimesheet(Func<Timesheet, bool> filter)
    {
        return 
            TimesheetWithUserAndMission()
                .Where(filter)
                .AsEnumerable();
    }
}
