using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.IRepositories;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public static class DI
    {
        public static void AddInfrastructure(this IServiceCollection service, IConfiguration config)
        {
            service.AddScoped<IUserDataAccess, UserDataAccess>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
