using BuildingConfiguration.Domain.Aggregates.BuildingAggregate;
using BuildingConfiguration.Infrastructure.EventHandlers;
using MediatR;
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
                var mediator = provider.GetRequiredService<IMediator>();
                return new BuildingRepository(mongoClient.GetDatabase("BuildingApi"), mediator);
            });

            services.AddScoped<ForwardDomainEventsToQueueEventHandler>();

            return services;
        }
    }
}