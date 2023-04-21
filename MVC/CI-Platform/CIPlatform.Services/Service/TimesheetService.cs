using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;

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
            MissionType = (MissionTypeEnum)(timesheet.Mission.MissionType ? 1 : 0),
            UserName = $"{timesheet.User.FirstName} {timesheet.User.LastName}",
            Message = timesheet.Notes?? string.Empty
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
    public void SaveUserVolunteerGoals(VolunteerGoalVM vlGoal)
    {
        Timesheet timesheetEntry = ConvertVolunteerGoalVMToTimesheetModel(vlGoal);
        _unitOfWork.TimesheetRepo.Add( timesheetEntry );
        _unitOfWork.Save();
    }
    
    public VolunteerHourVM LoadUserTimesheetEntry(long timesheetId, MissionTypeEnum missionType)
    {
        Timesheet timesheet = FetchTimesheetById(timesheetId);
        if (timesheet == null) return null!;
        return ConvertTimesheetModelToHourVM(timesheet);
    }

    public void UpdateUserTimesheetEntry(VolunteerHourVM vm)
    {
        var timesheet = FetchTimesheetById(vm.TimesheetId);

        timesheet.MissionId = vm.MissionId;
        timesheet.DateVolunteered = vm.Date;
        timesheet.Time = new TimeSpan( vm.Hours?? 0, vm.Minutes ?? 0, 0 );
        timesheet.Notes = vm.Message;
        _unitOfWork.Save();
    }


    private Timesheet FetchTimesheetById(long id) => 
        _unitOfWork.TimesheetRepo.GetFirstOrDefault(entry => entry.TimesheetId == id);
    public VolunteerGoalVM LoadUserGoalEntry(long timesheetId)
    {
        var timesheet = FetchTimesheetById(timesheetId);
        if (timesheet == null) return null!;

        return ConvertTimesheetModelToGoalVM(timesheet);
    }

    public void UpdateUserTimesheetEntry(VolunteerGoalVM vm)
    {
        var timesheetEntry = FetchTimesheetById( vm.TimesheetId );

        timesheetEntry.MissionId = vm.MissionId;
        timesheetEntry.Action = vm.Action;
        timesheetEntry.DateVolunteered = vm.Date;
        timesheetEntry.Notes = vm.Message;
        _unitOfWork.Save();
    }

    public void DeleteUserTimesheetEntry(long timesheetId)
    {
        _unitOfWork.TimesheetRepo.DeleteTimesheetEntry(timesheetId);
    }

    public IEnumerable<VolunteerTimesheetVM> LoadHourTimesheet(MissionTypeEnum missionType)
    {
        bool checkFor = missionType != MissionTypeEnum.GOAL;
        Func<Timesheet, bool> filter = timesheet => timesheet.Mission.MissionType == checkFor;
        IEnumerable<Timesheet> timesheet = _unitOfWork.TimesheetRepo.LoadTimesheet(filter);
        
        return
            timesheet
                .Select(ConvertTimesheetToVolunteerTimesheetVM);
    }

    public void UpdateTimesheetStatus(long timesheetId, byte status)
    {
        int result = _unitOfWork.TimesheetRepo.UpdateTimesheetApprovalStatus(timesheetId, status);
        if (result == 0) throw new Exception("Something went wrong during update timesheet status!");
    }

    public IEnumerable<VolunteerTimesheetVM> SearchTimesheet(string searchKey, MissionTypeEnum missionTypeEnum)
    {
        bool checkFor = missionTypeEnum != MissionTypeEnum.GOAL;
        Func<Timesheet, bool> filter;
        if(string.IsNullOrEmpty(searchKey))
            filter = timesheet => timesheet.Mission.MissionType == checkFor;
        else
            filter = timesheet => (timesheet.Mission.MissionType == checkFor) && 
                                  (timesheet.User.FirstName.ContainsCaseInsensitive(searchKey) || timesheet.User.LastName.ContainsCaseInsensitive(searchKey)
                                  || timesheet.Mission.Title!.ContainsCaseInsensitive(searchKey));

        IEnumerable<Timesheet> timesheet = _unitOfWork.TimesheetRepo.LoadTimesheet(filter);
        return
            timesheet
                .Select(ConvertTimesheetToVolunteerTimesheetVM);
    }

    public VolunteerTimesheetVM ViewTimesheetEntry(long timesheetId)
    {
        Func<Timesheet, bool> filter = entry => entry.TimesheetId == timesheetId;
        var timesheet = _unitOfWork.TimesheetRepo.FetchTimesheetEntry( filter );
        if (timesheet == null) throw new ArgumentNullException(nameof(timesheet));

        return ConvertTimesheetToVolunteerTimesheetVM(timesheet);
    }

    private static Timesheet ConvertVolunteerGoalVMToTimesheetModel(VolunteerGoalVM vlGoal)
    {
        Timesheet timesheet = new()
        {
            UserId = vlGoal.UserId,
            MissionId = vlGoal.MissionId,
            Notes = vlGoal.Message,
            DateVolunteered = vlGoal.Date,
            CreatedAt = DateTimeOffset.Now,
            Action = vlGoal.Action
        };
        return timesheet;
    }

    private static VolunteerHourVM ConvertTimesheetModelToHourVM(Timesheet timesheet)
    {
        VolunteerHourVM vm = new()
        {
            TimesheetId = timesheet.TimesheetId,
            UserId = timesheet.UserId,
            MissionId = timesheet.MissionId,
            Hours = timesheet.Time?.Hours,
            Minutes = timesheet.Time?.Minutes,
            Message = timesheet.Notes ?? string.Empty,
            Date = (DateTimeOffset)timesheet.DateVolunteered!,
        };
        return vm;
    }

    private static VolunteerGoalVM ConvertTimesheetModelToGoalVM(Timesheet timesheet)
    {
        VolunteerGoalVM vm = new()
        {
            TimesheetId = timesheet.TimesheetId,
            UserId = timesheet.UserId,
            MissionId = timesheet.MissionId,
            Action = timesheet.Action,
            Date = (DateTimeOffset)timesheet.DateVolunteered!,
            Message = timesheet.Notes?? string.Empty
        };
        return vm;
    }
}
