using N5Permission.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5Permission.Infrastructure.Persistance;
using N5Permission.Api.Installers.Helpers;
using N5Permission.Infrastructure.CrossCutting;

namespace N5Permission.IntegrationTests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((builder, services) =>
        {
            services
                .Remove<DbContextOptions<N5PermissionContext>>();

            ConfigureServices(services, builder.Configuration);
        });
    }
    void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddServicesInAssembly(configuration);
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

    }
}
