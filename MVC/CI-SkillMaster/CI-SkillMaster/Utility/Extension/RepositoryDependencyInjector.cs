using CISkillMaster.DataAccessLayer.Abstract;
using CISkillMaster.DataAccessLayer.Implementation;

namespace CI_SkillMaster.Utility.Extension;

public static class RepositoryDependencyInjector
{
    public static void RepositoryDependencyHelper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
    }
}
