using Microsoft.Extensions.Options;
using N5Permission.Api.Contracts;
using N5Permission.Application.Common;
using N5Permission.Infrastructure.CrossCutting;

namespace N5Permission.Api.Installers
{
    internal class RegisterAppSettings : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSetting>(appSettingsSection);
            services.AddSingleton(cfg => cfg.GetService<IOptions<AppSetting>>().Value);
        }
    }
}
