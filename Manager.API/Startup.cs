using Manager.CrossCutting;
using Manager.Infra.Persistence.DataContext;
using Manager.SharedKernel;
using Manager.SharedKernel.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ManagerDataContext.Update(Configuration.GetConnectionString("BitzenConnectionString"));

            services.AddScoped(_ => new ManagerDataContext(Configuration.GetConnectionString("BitzenConnectionString")));
            services.AddScoped<IHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddServices();

            services.AddCors();

            services.AddMvc()
                .AddJsonOptions(o =>
                {
                    o.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                    ((Newtonsoft.Json.Serialization.DefaultContractResolver)o.SerializerSettings.ContractResolver).NamingStrategy.ProcessDictionaryKeys = true;
                    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
               .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            DomainEvent.Container = services.BuildServiceProvider();
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            //app.UseAuthentication();
            app.UseMvc();
        }
    }
}
