using CISkillMaster.DataAccessLayer.Abstract;
using CISkillMaster.DataAccessLayer.Implementation;
using CISkillMaster.Services.Abstract;
using CISkillMaster.Services.Implementation;

namespace CI_SkillMaster.Utility.Extension;

public static class RepositoryDependencyInjector
{
    public static void RepositoryDependencyHelper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<ISkillRepository, SkillRepository>();

        serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
