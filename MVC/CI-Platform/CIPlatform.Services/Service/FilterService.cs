using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Services.Service;
internal class FilterService
{
    private IQueryable<MissionCardVM> missions;
    private FilterModel filterModel;
    public FilterService(IQueryable<MissionCardVM> missions, FilterModel filterModel)
    {
        this.missions = missions;
        this.filterModel = filterModel; 
    }

    internal IQueryable<MissionCardVM> FilterCriteria()
    {
        missions = SearchBySearchKeyword();
        missions = FilterByCountry();
        missions = FilterByCity();
        missions = FilterByTheme();
        missions = FilterBySkill1();
        missions = FilterBySortByMenu();

        return missions;
    }

    private IQueryable<MissionCardVM> CountCheck( IQueryable<MissionCardVM> result) => result.Count() == 0 ? missions : result;
    public IQueryable<MissionCardVM> FilterBySkill1()
    {
        if (filterModel.SkillList.Length == 0) return missions;
        List<MissionCardVM> filteredMissions = new();

        for (int i = 0; i < filterModel.SkillList.Length; i++)
        {
            short skillId = (short)filterModel.SkillList[i];

            foreach (MissionCardVM msn in missions)
            {
                if ( msn.SkillId != null && msn.SkillId.Any() && msn.SkillId.Contains(skillId) )
                {
                    filteredMissions.Add(msn);
                }
            }
        }

        return filteredMissions.AsQueryable();
    }
    public IQueryable<MissionCardVM> FilterBySkill()
    {
        Console.WriteLine("Length: " + filterModel.SkillList.Length);
        if (filterModel.SkillList != null && filterModel.SkillList.Length > 0 && filterModel.SkillList!.Any())
        {
            int n = filterModel.SkillList.Length;
            for(int i = 0; i < n; i++)
            {
                Console.WriteLine("--> " + filterModel.SkillList[i]);
                missions = missions.Where(msn => msn.SkillId!.Contains((short)filterModel.SkillList[i] ) );
                Console.WriteLine( missions );
            }
            return missions;
        }
        return missions;
    }

    internal IQueryable<MissionCardVM> FilterByTheme()
    {
        if (filterModel.ThemeList != null && filterModel.ThemeList.Length > 0 && filterModel.ThemeList!.Any())
        {
            missions = missions.Where(msn => filterModel.ThemeList.ToList().Contains((short)msn.ThemeId!));
        }
        return missions;
    }

    internal IQueryable<MissionCardVM> FilterByCity()
    {
        var filterMissions = missions;
        if (filterModel.CityList != null && filterModel.CityList.Length > 0 && filterModel.CityList!.Any())
        {
            filterMissions = filterMissions.Where(msn => filterModel.CityList.ToList().Contains((int)msn.CityId!));
        }
        return filterMissions;
    }
    internal IQueryable<MissionCardVM> FilterByCountry()
    {
        var filterMissions = missions;
        if ( filterModel.CountryList != null && filterModel.CountryList.Length > 0 && filterModel.CountryList.Any() )
        {
            filterMissions = filterMissions.Where( msn => filterModel.CountryList.ToList().Contains((byte)msn.CountryId!)  );
        }
        return filterMissions;
    }

    internal IQueryable<MissionCardVM> SearchBySearchKeyword()
    {
        StringComparison comp = StringComparison.OrdinalIgnoreCase;
        if (filterModel != null)
        {
            if (!string.IsNullOrEmpty(filterModel.SearchKeyword))
            {
                missions = missions.Where( msn => 
                        ( msn.Title!.Contains(filterModel.SearchKeyword, comp) ||
                          msn.ThemeName!.Contains(filterModel.SearchKeyword, comp) ||
                          msn.OrganizationName!.Contains(filterModel.SearchKeyword, comp)
                        ) 
                    );
            }
        }
        return missions;
    }

    internal IQueryable<MissionCardVM> FilterBySortByMenu()
    {
        if( filterModel.SortBy.HasValue )
        {
            switch( filterModel.SortBy )
            {
                case SortByMenu.NEWEST:
                    missions = missions.OrderBy( msn => msn.StartDate.HasValue ).OrderByDescending( msn => msn.StartDate );
                    break;

                case SortByMenu.OLDEST:
                    missions = missions.OrderByDescending(msn => msn.StartDate.HasValue).OrderBy( msn => msn.StartDate );
                    break;

                case SortByMenu.LOWEST_SEAT_AVAILABLE:
                    missions = missions.Where(msn => msn.Status == MissionStatus.ONGOING).OrderByDescending(msn => msn.SeatLeft.HasValue).OrderBy( msn => msn.SeatLeft );
                    break;

                case SortByMenu.HIGHEST_SEAT_AVAILABLE:
                    missions = missions.Where(msn => msn.Status == MissionStatus.ONGOING).OrderBy( msn => msn.SeatLeft.HasValue ).OrderByDescending( msn => msn.SeatLeft );
                    break;

                case SortByMenu.FAVOURITE:
                    break;

                case SortByMenu.REGISTRATION_DEADLINE:
                    missions = missions.Where(msn => msn.Status == MissionStatus.ONGOING).OrderBy(msn => msn.RegistrationDeadline.HasValue).OrderByDescending(msn => msn.RegistrationDeadline);
                    break;
            }
        }
        return missions;
    }
}
