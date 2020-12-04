using System.Collections.Immutable;
using BuildingConfiguration.Domain.Aggregates.BuildingAggregate;
using Invoicing.Base.Ddd;
using MongoDB.Bson.Serialization;
using NodaTime;

namespace BuildingConfiguration.Infrastructure
{
    public static class BuildingCollectionConfigurator
    {
        public static void SetupClassMaps()
        {
            BsonSerializer.RegisterGenericSerializerDefinition(typeof(ImmutableList<>), typeof(ImmutableListSerializer<>));
            BsonSerializer.RegisterSerializer(typeof(Instant), new InstantSerializer());

            BsonClassMap.RegisterClassMap<Entity>(classMapInitializer =>
            {
                classMapInitializer.MapProperty(entity => entity.Id);
            });

            BsonClassMap.RegisterClassMap<Building>(classMapInitializer =>
            {
                classMapInitializer.MapProperty(building => building.Name);
                classMapInitializer.MapProperty(building => building.Location);
                classMapInitializer.MapProperty(building => building.Meters);

                classMapInitializer.MapCreator(building => new Building(building.Name, building.Location, building.Meters));
            });

            BsonClassMap.RegisterClassMap<BuildingLocation>(classMapInitializer =>
            {
                classMapInitializer.MapProperty(location => location.PostalCode);
                classMapInitializer.MapProperty(location => location.City);
                classMapInitializer.MapProperty(location => location.Country);

                classMapInitializer.MapCreator(location => new BuildingLocation(location.City, location.PostalCode, location.Country));
            });

            BsonClassMap.RegisterClassMap<Meter>(classMapInitializer =>
            {
                classMapInitializer.MapProperty(meter => meter.EanCode);
                classMapInitializer.MapProperty(meter => meter.MeterType).SetSerializer(new SmartEnumSerializer<MeterType>());
                classMapInitializer.MapProperty(meter => meter.Registers);

                classMapInitializer.MapCreator(meter => new Meter(meter.EanCode, meter.MeterType, meter.Registers));
            });

            BsonClassMap.RegisterClassMap<Register>(classMapInitializer =>
            {
                classMapInitializer.MapProperty(register => register.Tariff).SetSerializer(new SmartEnumSerializer<Tariff>());
                classMapInitializer.MapProperty(register => register.LastReading);
                classMapInitializer.MapProperty(register => register.LastReadingRegisteredOn);

                classMapInitializer.MapCreator(register => new Register(register.Tariff));
            });
        }
    }
}