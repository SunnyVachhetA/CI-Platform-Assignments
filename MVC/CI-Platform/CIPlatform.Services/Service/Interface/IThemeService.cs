using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IThemeService
{
    List<ThemeVM> GetAllThemes();
    IEnumerable<ThemeVM> SearchThemeByKey(string searchKey);
    bool CheckThemeIsUnique(string themeName, short themeId);
    void AddTheme(ThemeVM themeVm);
    void DeleteThemeById(short themeId);
    void DeActivateThemeById(short themeId);
}
