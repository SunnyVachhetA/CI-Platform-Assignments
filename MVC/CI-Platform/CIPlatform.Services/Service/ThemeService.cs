using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;

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

    public IEnumerable<ThemeVM> SearchThemeByKey(string searchKey)
    {
        if (string.IsNullOrEmpty(searchKey)) return GetAllThemes();
        return 
            GetAllThemes()
            .Where(theme => theme.Title!.ContainsCaseInsensitive(searchKey));
    }

    private IEnumerable<MissionTheme> GetAllThemesModel() => _unitOfWork.ThemeRepo.GetAll();
    public bool CheckThemeIsUnique(string themeName, short themeId)
    {
        if (themeId == 0)
        {
            return GetAllThemesModel().FirstOrDefault( theme => theme.Title!.EqualsIgnoreCase(themeName) ) == null;
        }

        return !GetAllThemesModel().Any( theme => theme.Title!.EqualsIgnoreCase(themeName) && themeId != theme.ThemeId);
    }

    public void AddTheme(ThemeVM themeVm)
    {
        MissionTheme theme = new()
        {
            Title = themeVm.Title,
            Status = themeVm.Status,
            CreatedAt = DateTimeOffset.Now
        };

        _unitOfWork.ThemeRepo.Add(theme);
        _unitOfWork.Save();
    }

    public void DeleteThemeById(short themeId)
    {
        int numOfrow = _unitOfWork.ThemeRepo.DeleteTheme(themeId);
        if (numOfrow == 0) throw new Exception("Something went during theme delete");
    }
    
    public ThemeVM GetThemeDetails(short themeId)
    {
        var theme = _unitOfWork.ThemeRepo.GetFirstOrDefault(theme => theme.ThemeId == themeId);
        return ConvertThemeToViewModel(theme);
    }

    public void EditTheme(ThemeVM themeVm)
    {
        var theme = _unitOfWork.ThemeRepo.GetFirstOrDefault( theme => theme.ThemeId == themeVm.ThemeId );
        if (theme == null) throw new Exception("Theme Update Error!");

        theme.Title = themeVm.Title;
        theme.Status = themeVm.Status;
        theme.UpdatedAt = DateTimeOffset.Now;

        _unitOfWork.Save();
    }

    public void UpdateThemeStatus(short themeId, byte status = 0)
    {

        int numOfRow = _unitOfWork.ThemeRepo.UpdateThemeStatus(themeId, status);
        if (numOfRow == 0) throw new Exception("Something went during theme status update");
    }

    public ThemeVM ConvertThemeToViewModel(MissionTheme theme)
    {
        ThemeVM themeVm = new()
        {
            ThemeId = theme.ThemeId,
            Status = theme.Status,
            Title = theme.Title,
            LastModified = LastModifiedThemeDate(theme)    
        };
        return themeVm;
    }

    public DateTimeOffset LastModifiedThemeDate(MissionTheme theme)
    {
        return new[] { theme.CreatedAt, theme.UpdatedAt, theme.DeletedAt }
            .Max() ?? theme.CreatedAt;
    }
}
