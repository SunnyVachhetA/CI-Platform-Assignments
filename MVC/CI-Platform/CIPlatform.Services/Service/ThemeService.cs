using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class ThemeService : IThemeService
{
    private readonly IUnitOfWork _unitOfWork;

    public ThemeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<ThemeVM> GetAllThemes()
    {
        var result = _unitOfWork.ThemeRepo.GetAll();
        List<ThemeVM> themeList = new();
        foreach (var theme in result)
            themeList.Add( ConvertThemeToViewModel(theme) );
        return themeList;
    }

    public ThemeVM ConvertThemeToViewModel(MissionTheme theme)
    {
        ThemeVM themeVm = new()
        {
            ThemeId = theme.ThemeId,
            Status = theme.Status,
            Title = theme.Title,
        };
        return themeVm;
    }
}
