using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Contracts.Services;
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

            services.AddScoped<IFuelRepository, FuelRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVendorRepository, VendorRepoitory>();
            services.AddScoped<IOpportunityRepository, OpportunityRepository>();

            services.AddScoped<IFuelService, FuelService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IVendorService, VendorService>();
            services.AddScoped<IOpportunityService, OpportunityService>();
        }
    }
}
