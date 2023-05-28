using FluentValidation; 
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace N5Permission.Infrastructure.CrossCutting
{
    public static class RegisterService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); 
            });

            return services;
        }
    }
}
