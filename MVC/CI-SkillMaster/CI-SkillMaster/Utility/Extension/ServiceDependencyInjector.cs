using CISkillMaster.Services.Abstract;
using CISkillMaster.Services.Implementation;

namespace CI_SkillMaster.Utility.Extension;

public static class ServiceDependencyInjector
{
    public static void ServiceDependencyHelper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<ISkillService, SkillService>();

        //serviceCollection.AddScoped(typeof(IService<>), typeof(Service<>));

    }

}
