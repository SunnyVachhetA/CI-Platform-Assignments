﻿using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Services.Service.Interface;
public interface ITimesheetService
{
    List<VolunteerTimesheetVM> LoadUserTimesheet(long id);
    void SaveUserVolunteerHours(VolunteerHourVM volunteerHour);
    IEnumerable<VolunteerTimesheetVM> LoadUserTimesheet(long userId, MissionTypeEnum missionType);
    void SaveUserVolunteerGoals(VolunteerGoalVM vlGoal);
    VolunteerHourVM LoadUserTimesheetEntry(long timesheetId, MissionTypeEnum missionType);
    void UpdateUserTimesheetEntry(VolunteerHourVM vm);
    VolunteerGoalVM LoadUserGoalEntry(long timesheetId);
    void UpdateUserTimesheetEntry(VolunteerGoalVM vm);
    void DeleteUserTimesheetEntry(long timesheetId);
}