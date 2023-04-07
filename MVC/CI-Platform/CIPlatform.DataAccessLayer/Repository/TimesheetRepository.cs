using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class TimesheetRepository: Repository<Timesheet>, ITimesheetRepository
{
    private readonly CIDbContext _dbContext;
    public TimesheetRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
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

    public void DeleteTimesheetEntry(long timesheetId)
    {
        var idParam = new SqlParameter("@id", timesheetId);

        _dbContext.Database.ExecuteSqlRaw("DELETE FROM timesheet WHERE timesheet_id = @id", idParam);
    }
}
