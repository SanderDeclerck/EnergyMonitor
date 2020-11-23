using BuildingConfiguration.Domain.Aggregates.BuildingAggregate;
using Invoicing.Base.Ddd;
using MongoDB.Bson.Serialization;

namespace BuildingConfiguration.Infrastructure
{
    public static class BuildingCollectionConfigurator
    {
        static BuildingCollectionConfigurator()
        {
            BsonClassMap.RegisterClassMap<Entity>(classMapInitializer => 
            {
                classMapInitializer.MapProperty(entity => entity.Id);
            });

            BsonClassMap.RegisterClassMap<Building>(classMapInitializer => 
            {
                classMapInitializer.MapProperty(building => building.Name);
                classMapInitializer.MapProperty(building => building.Location);

                classMapInitializer.MapCreator(building => new Building(building.Name, building.Location));
            });

            BsonClassMap.RegisterClassMap<BuildingLocation>(classMapInitializer => 
            {
                classMapInitializer.MapProperty(location => location.PostalCode);
                classMapInitializer.MapProperty(location => location.City);
                classMapInitializer.MapProperty(location => location.Country);
            });
        }
    }
}