using N5Permission.Api.Contracts;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace N5Permission.Api.Installers
{
    internal class RegisterSerilog : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment!))
                .Enrich.WithProperty("Environment", environment!)
                .ReadFrom.Configuration(configuration)
                .CreateLogger(); 
        }

        ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"n5Permission-api-{environment?.ToLower().Replace(".", "-")}"
            };
        }
    }
}
