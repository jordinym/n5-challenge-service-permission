
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5Permission.Api.Installers.Helpers;
using N5Permission.Infrastructure.CrossCutting;

namespace N5Permission.IntegrationTests
{
    public class PermissionWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        IConfiguration configuration;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            

            builder.ConfigureAppConfiguration((hostingContext, config) =>
             {
                 config
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                 .AddEnvironmentVariables();

                 configuration = config.Build();

             }).ConfigureServices(services =>
            { 
                ConfigureServices(services, configuration);
                 
            });

            builder.UseEnvironment("Development");
        }

        void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddServicesInAssembly(configuration);
            services.AddApplicationServices();
            services.AddInfrastructureServices(configuration);

        }
    }
}
