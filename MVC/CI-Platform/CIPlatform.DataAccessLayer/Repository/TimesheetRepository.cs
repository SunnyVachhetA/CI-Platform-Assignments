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

    public IEnumerable<Timesheet> LoadTimesheet(Func<Timesheet, bool> filter)
    {
        return
            TimesheetWithUserAndMission()
                .AsEnumerable()
                .Where(filter);
    }

    public int UpdateTimesheetApprovalStatus(long timesheetId, byte status)
    {
        var query = "UPDATE timesheet SET status = {0} WHERE timesheet_id = {1}";
        return _dbContext.Database.ExecuteSqlRaw(query, status, timesheetId);
    }

    public Timesheet? FetchTimesheetEntry(Func<Timesheet, bool> filter)
    {
        return
            TimesheetWithUserAndMission()
                .AsEnumerable()
                .FirstOrDefault(filter);
    }

    public Timesheet TimesheetWithGoalMission(long timesheetId)
    {
        return
            dbSet
                .Include(entry => entry.Mission)
                .ThenInclude(goal => goal.GoalMissions)
                .FirstOrDefault(entry => entry.TimesheetId == timesheetId)!;
    }
}
