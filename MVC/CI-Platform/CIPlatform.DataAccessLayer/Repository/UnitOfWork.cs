using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;

namespace CIPlatform.DataAccessLayer.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly CIDbContext _dbContext;
    public IUserRepository UserRepo { get; private set; }
    public IPasswordResetRepository PasswordResetRepo { get; private set; }
    public ICountryRepository CountryRepo { get; private set; }
    public ICityRepository CityRepo { get; private set; }
    public ISkillRepository SkillRepo { get; private set; }
    public IThemeRepository ThemeRepo { get; private set; }

    public IMissionRepository MissionRepo { get; private set; }

    public IFavouriteMissionRepository FavouriteMissionRepo { get; private set; }

    public IMissionApplicationRepository MissionApplicationRepo { get; private set; }

    public IGoalMissionRepository GoalMissionRepo { get; private set; }

    public IMissionMediaRepository MissionMediaRepo { get; private set; }

    public IMissionSkillRepository MissionSkillRepo { get; private set; }

    public UnitOfWork(CIDbContext dbContext)
    {
        _dbContext = dbContext;
        UserRepo = new UserRepository(_dbContext);
        PasswordResetRepo = new PasswordResetRepository(_dbContext);
        CountryRepo = new CountryRepository(_dbContext);
        CityRepo = new CityRepository(_dbContext);    
        SkillRepo = new SkillRepository(_dbContext);
        ThemeRepo = new ThemeRepository(_dbContext);
        MissionRepo = new MissionRepository(_dbContext);
        FavouriteMissionRepo = new FavouriteMissionRepository(_dbContext);
        MissionApplicationRepo = new MissionApplicationRepository(_dbContext);
        GoalMissionRepo = new GoalMissionRepository(_dbContext);
        MissionMediaRepo = new MissionMediaRepository(_dbContext);
        MissionSkillRepo = new MissionSkillRepository(_dbContext);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}
