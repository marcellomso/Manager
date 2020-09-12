using Manager.API.Auth;
using Manager.CrossCutting;
using Manager.Infra.Persistence.DataContext;
using Manager.SharedKernel;
using Manager.SharedKernel.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddServices();

            services.AddSwaggerGen();

            services.AddCors();
            services.AddAuth(Configuration);
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            })
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BizenManager API V1");
            });

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
