
namespace CIPlatform.Entities.ViewModels;
public class FilterModel
{
    public byte[]? CountryList { get; set; }
    public int[]? CityList { get; set; }

    public short[]? ThemeList { get; set; }
    public short[]? SkillList { get; set; }

    public string? SearchKeyword { get; set; }

    public byte? SortBy { get; set; }
}
