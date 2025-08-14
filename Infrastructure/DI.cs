using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.IRepositories;
using Infrastructure.Repositories;
using Application.ICommonInterfaces;
using Infrastructure.Security;

namespace Infrastructure
{
    public static class DI
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUserDataAccess, UserDataAccess>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IJWTManager<>), typeof(JWTManager<>));
        }
    }
}
