using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class TimesheetService: ITimesheetService
{
    private readonly IUnitOfWork _unitOfWork;
    public TimesheetService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private Func<Timesheet, bool> FilterUserTimesheet(long id)
    {
        return timesheet => timesheet.UserId == id;
    }

    private Func<Timesheet, bool> FilterUserTimesheet(long id, bool checkFor)
    {
        return timesheet => timesheet.UserId == id && timesheet.Mission.MissionType == checkFor;
    }

    public List<VolunteerTimesheetVM> LoadUserTimesheet(long id)
    {
        Func<Timesheet, bool> filter = FilterUserTimesheet(id);
        IEnumerable<Timesheet> timesheet =
            _unitOfWork.TimesheetRepo.GetUserTimesheet( filter );

        List<VolunteerTimesheetVM> list = new();
        
        if (!timesheet.Any()) return list;

        list =
            timesheet
                .Select( ConvertTimesheetToVolunteerTimesheetVM )
                .ToList();

        return list;
    }

    public static VolunteerTimesheetVM ConvertTimesheetToVolunteerTimesheetVM(Timesheet timesheet)
    {
        VolunteerTimesheetVM timesheetVM = new()
        {
            TimesheetId = timesheet.TimesheetId,
            MissionId = timesheet.MissionId,
            UserId = timesheet.UserId,
            MissionTitle = timesheet.Mission.Title?? string.Empty,
            Time = timesheet.Time,
            Status = (ApprovalStatus)(timesheet.Status?? 0),
            Date = timesheet.CreatedAt,
            Action = timesheet.Action,
            MissionType = (MissionTypeEnum)(timesheet.Mission.MissionType ? 1 : 0)
        };

        return timesheetVM;
    }



    public void SaveUserVolunteerHours(VolunteerHourVM volunteerHour)
    {
        var timesheetEntry = ConvertVolunteerHourVMToTimesheetModel(volunteerHour);
        _unitOfWork.TimesheetRepo.Add(timesheetEntry);
        _unitOfWork.Save();
    }

    public IEnumerable<VolunteerTimesheetVM> LoadUserTimesheet(long userId, MissionTypeEnum missionType)
    {
        bool checkFor = missionType != MissionTypeEnum.GOAL;
        var timesheet = _unitOfWork.TimesheetRepo.GetUserTimesheet(FilterUserTimesheet(userId, checkFor));

        IEnumerable<VolunteerTimesheetVM> timesheetEntry = new List<VolunteerTimesheetVM>();

        if (timesheet.Any())
        {
            timesheetEntry =
                timesheet
                    .Select(ConvertTimesheetToVolunteerTimesheetVM);
        }            
        return timesheetEntry;
    }

    private Timesheet ConvertVolunteerHourVMToTimesheetModel(VolunteerHourVM volunteerHour)
    {
        Timesheet timesheet = new()
        {
            UserId = volunteerHour.UserId,
            MissionId = volunteerHour.MissionId,
            Notes = volunteerHour.Message,
            Time = new TimeSpan(volunteerHour.Hours?? 0, volunteerHour.Minutes?? 0, 00),
            DateVolunteered = volunteerHour.Date,
            CreatedAt = DateTimeOffset.Now
        };
        return timesheet;
    }
}
