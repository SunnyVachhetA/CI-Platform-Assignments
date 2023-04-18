using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Utilities;

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

    public IEnumerable<MissionVMCard> Filter()
    {
        missions = FilterBySearch();
        missions = FilterByCountry();
        missions = FilterByCity();
        missions = FilterByTheme();
        missions = FilterBySkill();
        missions = FilterBySortMenu();
        return
            missions
                .Skip((filterModel.Page - 1) * 9)
                .Take(9);
    }

    public IQueryable<MissionVMCard> FilterByCity()
    {
        if( ! filterModel.CityList.Any() ) return missions;

        return
            missions
                .Where( msn => filterModel.CityList.Any(id => id == msn.CityId) );
    }

    public IQueryable<MissionVMCard> FilterByCountry()
    {
        if (!filterModel.CountryList.Any()) return missions;
        return
            missions
                .Where( msn => filterModel.CountryList.Any( id => id == msn.CountryId ) );
    }

    public IQueryable<MissionVMCard> FilterByTheme()
    {
        if (!filterModel.ThemeList.Any()) return missions;
        return
            missions
                .Where(msn => filterModel.ThemeList.Any(id => id == msn.ThemeId));
    }

    public IQueryable<MissionVMCard> FilterBySkill()
    {
        if (!filterModel.SkillList.Any()) return missions;

        return
            missions
                .Where(msn => msn.MissionSkill.Any(id => filterModel.SkillList.Contains(id)) );
    }

    public IQueryable<MissionVMCard> FilterBySearch()
    {
        if (string.IsNullOrEmpty(filterModel.SearchKeyword) || string.IsNullOrWhiteSpace(filterModel.SearchKeyword))
            return missions;

        return
            missions
                .Where( msn => msn.Title.ContainsCaseInsensitive(filterModel.SearchKeyword) 
                               || 
                            msn.OrganizationName!.ContainsCaseInsensitive(filterModel.SearchKeyword)  );
    }

    public IQueryable<MissionVMCard> FilterBySortMenu()
    {
        if (filterModel.SortBy.HasValue)
        {
            switch (filterModel.SortBy)
            {
                case SortByMenu.NEWEST:
                    missions = missions.OrderBy(msn => msn.StartDate.HasValue);
                    break;

                case SortByMenu.OLDEST:
                    missions = missions.OrderByDescending(msn => msn.StartDate.HasValue);
                    break;

                case SortByMenu.LOWEST_SEAT_AVAILABLE:
                    missions = missions.Where(msn => msn.Status == MissionStatus.ONGOING)
                        .OrderByDescending(msn => msn.SeatLeft.HasValue);
                    break;

                case SortByMenu.HIGHEST_SEAT_AVAILABLE:
                    missions = missions.Where(msn => msn.Status == MissionStatus.ONGOING).OrderBy(msn => msn.SeatLeft.HasValue);
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
                    missions = missions.Where(msn => msn.Status == MissionStatus.ONGOING).OrderBy(msn => msn.RegistrationDeadline.HasValue);
                    break;
            }
        }
        return missions;
    }
}
