namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IUnitOfWork
{
    IUserRepository UserRepo { get; }
    IPasswordResetRepository PasswordResetRepo { get; }
    ICountryRepository CountryRepo { get; }
    ICityRepository CityRepo { get; }
    ISkillRepository SkillRepo { get; }
    IThemeRepository ThemeRepo { get; }

    IMissionRepository MissionRepo { get; }
    IFavouriteMissionRepository FavouriteMissionRepo { get; }
    IMissionApplicationRepository MissionApplicationRepo { get; }   
    IGoalMissionRepository GoalMissionRepo { get; }
    IMissionMediaRepository MissionMediaRepo { get; }
    IMissionSkillRepository MissionSkillRepo { get; }
    void Save();
}
