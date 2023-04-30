
using CIPlatform.Entities.VMConstants;

namespace CIPlatform.Entities.ViewModels;
public class FilterModel
{
    public List<int> CountryList { get; set; } = new();
    public List<int> CityList { get; set; } = new();

    public List<int> ThemeList { get; set; } = new(); 
    public List<int> SkillList { get; set; } = new();

    public string? SearchKeyword { get; set; } 

    public SortByMenu? SortBy { get; set; }

    public ExploreMenu? ExploreBy { get; set; }
    public int Page { get; set; }
    public long? UserId { get; set; }
}
