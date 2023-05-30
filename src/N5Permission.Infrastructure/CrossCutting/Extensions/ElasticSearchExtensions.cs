using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5Permission.Application.Permission.Dto;
using Nest;

namespace N5Permission.Infrastructure.CrossCutting.Extensions
{
    public static class ElasticSearchExtensions
    { 
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["ElasticSearch:Uri"];
            var defaultIndex = configuration["ElasticSearch:Index"];

            var pool = new SingleNodeConnectionPool(new Uri(url));
            var settings = new ConnectionSettings(pool)
                .PrettyJson()
                .DefaultIndex(defaultIndex);

            AddDefaultMappings(settings, defaultIndex);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings, string indexName)
        {
            settings
                .DefaultMappingFor<PermissionChangeLog>(i => i
                .IndexName(indexName)
                .IdProperty(p => p.LogId)
                );
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<PermissionChangeLog>(x => x.AutoMap())
            );
        }
    }
}
