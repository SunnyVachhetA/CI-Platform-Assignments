
using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Entities.ViewModels;
public class FilterModel
{
    public int[]? CountryList { get; set; }
    public int[]? CityList { get; set; } 

    public int[]? ThemeList { get; set; } 
    public int[]? SkillList { get; set; }

    public string? SearchKeyword { get; set; } 

    public SortByMenu? SortBy { get; set; } 
}
