namespace CIPlatform.Services.Service.Interface;
public interface IServiceUnit
{
    IUserService UserService { get; }
    IPasswordResetService PasswordResetService { get; }
    ICountryService CountryService { get; }    
    ICityService CityService { get; }

    ISkillService SkillService { get; }
    IThemeService ThemeService { get; }
}
