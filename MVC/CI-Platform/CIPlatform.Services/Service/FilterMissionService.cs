using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.Services.Service;
public class FilterMissionService
{
    private IQueryable<MissionVMCard> missions;
    private FilterModel filterModel;

    public FilterMissionService(IEnumerable<MissionVMCard> missions, FilterModel filterModel)
    {
        this.missions = missions.AsQueryable();
        this.filterModel = filterModel;
    }

    public Task<(IEnumerable<MissionVMCard>, long)> Filter()
    {
        missions = FilterBySearch();
        missions = FilterByCountry();
        missions = FilterByCity();
        missions = FilterByTheme();
        missions = FilterBySkill();
        missions = FilterBySortMenu();
        missions = FilterByExploreMenu();
        var filterMissions =
            missions
                .Skip((filterModel.Page - 1) * 9)
                .Take(9);
        return Task.Run(() => (filterMissions.AsEnumerable(), missions.LongCount()));
    }

    private IQueryable<MissionVMCard> FilterByCity()
    {
        if (!filterModel.CityList.Any()) return missions;

        return
            missions
                .Where(msn => filterModel.CityList.Any(id => id == msn.CityId));
    }

    private IQueryable<MissionVMCard> FilterByCountry()
    {
        if (!filterModel.CountryList.Any()) return missions;
        return
            missions
                .Where(msn => filterModel.CountryList.Any(id => id == msn.CountryId));
    }

    private IQueryable<MissionVMCard> FilterByTheme()
    {
        if (!filterModel.ThemeList.Any()) return missions;
        return
            missions
                .Where(msn => filterModel.ThemeList.Any(id => id == msn.ThemeId));
    }

    private IQueryable<MissionVMCard> FilterBySkill()
    {
        if (!filterModel.SkillList.Any()) return missions;

        return
            missions
                .Where(msn => msn.MissionSkill.Any(id => filterModel.SkillList.Contains(id)));
    }

    private IQueryable<MissionVMCard> FilterBySearch()
    {
        if (string.IsNullOrEmpty(filterModel.SearchKeyword) || string.IsNullOrWhiteSpace(filterModel.SearchKeyword))
            return missions;

        return
            missions
                .Where(msn => msn.Title.ContainsCaseInsensitive(filterModel.SearchKeyword)
                               ||
                            msn.OrganizationName!.ContainsCaseInsensitive(filterModel.SearchKeyword));
    }

    private IQueryable<MissionVMCard> FilterBySortMenu()
    {
        if (!filterModel.SortBy.HasValue) return missions;
        switch (filterModel.SortBy)
        {
            case SortByMenu.NEWEST:
                missions = missions.OrderBy(msn => msn.StartDate.HasValue).OrderByDescending(msn => msn.StartDate);
                break;

            case SortByMenu.OLDEST:
                missions = missions.OrderByDescending(msn => msn.StartDate.HasValue).OrderBy(msn => msn.StartDate);
                break;

            case SortByMenu.LOWEST_SEAT_AVAILABLE:
                missions = missions.Where(msn => msn.Status == MissionStatus.ONGOING && msn.TotalSeat != null).OrderByDescending(msn => msn.SeatLeft.HasValue).OrderBy(msn => msn.SeatLeft);
                break;

            case SortByMenu.HIGHEST_SEAT_AVAILABLE:
                missions = missions.Where(msn => msn.Status == MissionStatus.ONGOING).OrderBy(msn => msn.SeatLeft.HasValue).OrderByDescending(msn => msn.SeatLeft);
                break;

            case SortByMenu.FAVOURITE:
                if (filterModel.UserId != 0)
                {
                    missions = missions.Where(msn => (msn.FavoriteMissionList.Contains((long)filterModel.UserId!)));
                }
                break;

            case SortByMenu.RESET:
                break;

            case SortByMenu.REGISTRATION_DEADLINE:
                missions = missions.Where(msn => msn.Status == MissionStatus.ONGOING && msn.RegistrationDeadline != null).OrderByDescending(msn => msn.RegistrationDeadline.HasValue).OrderBy(msn => msn.RegistrationDeadline);
                break;
            default: throw new ArgumentOutOfRangeException();
        }
        return missions;
    }

    private IQueryable<MissionVMCard> FilterByExploreMenu()
    {
        if (!filterModel.ExploreBy.HasValue) return missions;
        switch (filterModel.ExploreBy)
        {
            case ExploreMenu.MOST_RANKED:
                missions =
                    missions
                        .OrderByDescending(msn => msn.Rating);
                break;

            case ExploreMenu.TOP_THEMES:
                missions =
                    missions
                        .GroupBy(msn => msn.ThemeId)
                        .OrderByDescending(group => group.Count())
                        .SelectMany(msn => msn);
                break;

            case ExploreMenu.TOP_FAVOURITE:
                missions =
                    missions
                        .OrderByDescending(msn => msn.FavoriteMissionList.LongCount());
                break;

            case ExploreMenu.RANDOM:
                var random = new Random();
                missions =
                    missions
                        .OrderBy(msn => random.Next());
                break;
            default: throw new ArgumentOutOfRangeException();
        }
        return missions;
    }
}