using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//#if (AddHealthCheck)
using PROJECT_NAME.HealthChecks;
//#endif
using PROJECT_NAME.Middleware;

namespace PROJECT_NAME
{
    public class Startup
    {
        public Startup()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<AppConfig>(Configuration);
            //#if (AddHealthCheck)
            services.AddHealthChecks()
                .AddCheck<VersionHealthCheck>("Version Health Check");
            //#endif
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<HttpExceptionMiddleware>();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //#if (AddHealthCheck)
                endpoints.MapHealthChecks();
                //#endif
            });
        }
    }
}
