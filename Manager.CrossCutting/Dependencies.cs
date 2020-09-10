using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Contracts.Service;
using Manager.Infra.Persistence;
using Manager.Infra.Repositories;
using Manager.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.CrossCutting
{
    public static class Dependencies
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IRoleService, RoleService>();
        }
    }
}
