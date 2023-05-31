using N5Permission.Api.Installers.Helpers;
using N5Permission.Infrastructure.CrossCutting;
using N5Permission.Infrastructure.Persistance;
using Serilog;
using N5Permission.Api.Filters;
using FluentValidation.AspNetCore; 

namespace N5Permission.Api;

public class Program
{
    static IConfiguration configuration;
    public async static Task Main(string[] args)
    {


        var builder = WebApplication.CreateBuilder(args);

        configuration = builder.Configuration;
        // Add services to the container.

        builder.Services.AddControllers(opt => opt.Filters.Add<ApiExceptionFilterAttribute>())
                    .AddFluentValidation();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        ConfigureServices(builder.Services);

        builder.Host.UseSerilog();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {

        }
        app.UseSwagger();
        app.UseSwaggerUI();
        // Initialise database
        using (var scope = app.Services.CreateScope())
        {
            var initialiser = scope.ServiceProvider.GetRequiredService<N5PermissionContextInitializer>();
            await initialiser.InitialiseAsync();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");

        app.UseAuthorization();

        app.MapControllers();
        app.Run();

    }
    static void ConfigureServices(IServiceCollection services)
    {
        services.AddServicesInAssembly(configuration);
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

    }
}
 
 
