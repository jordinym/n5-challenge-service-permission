using Microsoft.Extensions.Configuration;
using N5Permission.Api.Installers.Helpers;
using N5Permission.Infrastructure.CrossCutting;
using N5Permission.Infrastructure.Persistance;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using System.Reflection;

IConfiguration configuration;

var builder = WebApplication.CreateBuilder(args);

configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureServices(builder.Services);

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

void ConfigureServices(IServiceCollection services)
{
    services.AddServicesInAssembly(configuration);
    services.AddApplicationServices();
    services.AddInfrastructureServices(configuration);
 
}
 
