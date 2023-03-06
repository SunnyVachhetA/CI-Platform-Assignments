using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IThemeService
{
    List<ThemeVM> GetAllThemes();
}
