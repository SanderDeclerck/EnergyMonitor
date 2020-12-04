using BuildingConfiguration.Domain.Aggregates.BuildingAggregate;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BuildingConfiguration.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SetupInfrastructure(this IServiceCollection services, string mongoConnectionstring)
        {
            BuildingCollectionConfigurator.SetupClassMaps();

            services.AddScoped<IMongoClient>(_ => new MongoClient(mongoConnectionstring));
            services.AddScoped<IBuildingRepository>(provider =>
            {
                var mongoClient = provider.GetRequiredService<IMongoClient>();
                return new BuildingRepository(mongoClient.GetDatabase("BuildingApi"));
            });

            return services;
        }
    }
}