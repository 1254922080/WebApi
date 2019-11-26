using LogicHandlers.ILogicHandlers;
using LogicHandlers.LogicHandlers;
using Microsoft.Extensions.DependencyInjection;
using Repositories.IRepositories;
using Repositories.Repositories;

namespace WebApi
{
    public static  class DependencyExtensions
    {
        public static void AddDependency(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepositories<>), typeof(BaseRepositories<>));
            services.AddTransient<IUserLogicHandler, UserLogicHandler>();
            services.AddTransient<IUserRepositories, UserRepositories>();

        }
    }
}
