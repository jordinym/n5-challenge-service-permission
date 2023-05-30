using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Infrastructure.CrossCutting.Extensions;
using N5Permission.Infrastructure.Persistance;
using N5Permission.Infrastructure.Persistance.Repositories;

namespace N5Permission.Infrastructure.CrossCutting
{
    public static class RegisterService
    {
        #region View Logs in concole
        private static readonly ILoggerFactory DbCommandConsoleLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        #endregion

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextConfig(configuration);
            services.AddPermissionServices(configuration);

            return services;
        }

        public static IServiceCollection AddDbContextConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<N5PermissionContext>(options =>
                options.UseLoggerFactory(DbCommandConsoleLoggerFactory) //enable only to view logs in console
                       .EnableSensitiveDataLogging() //enable only to view logs in console
                            .UseSqlServer(configuration["AppSettings:ConnectionStrings:DefaultConnection"],
                            b => b.MigrationsAssembly(typeof(N5PermissionContext).Assembly.FullName)
                            .CommandTimeout(480)
                            .MinBatchSize(5000)
                            .MaxBatchSize(10000)
                            ));

            services.AddScoped<N5PermissionContextInitializer>();

            return services;
        }

        public static IServiceCollection AddPermissionServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IPermissionTypeRepository, PermissionTypeRepository>();

            services.AddElasticsearch(configuration);

            return services;
        }
    }
}
