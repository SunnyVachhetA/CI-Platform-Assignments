using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IThemeService
{
    List<ThemeVM> GetAllThemes();
    IEnumerable<ThemeVM> SearchThemeByKey(string searchKey);
    bool CheckThemeIsUnique(string themeName, short themeId);
    void AddTheme(ThemeVM themeVm);
    void DeleteThemeById(short themeId);
    ThemeVM GetThemeDetails(short themeId);
    void EditTheme(ThemeVM themeVm);
    void UpdateThemeStatus(short themeId, byte status = 0);
    Task<IEnumerable<ThemeVM>> GetAllThemesAsync();
}
