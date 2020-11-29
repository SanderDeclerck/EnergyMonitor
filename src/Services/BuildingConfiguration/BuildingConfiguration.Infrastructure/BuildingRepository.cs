using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BuildingConfiguration.Domain.Aggregates.BuildingAggregate;
using MongoDB.Driver;

namespace BuildingConfiguration.Infrastructure
{
    public class BuildingRepository : IBuildingRepository
    {
        private const string MongoCollectionName = "Buildings";
        private readonly IMongoDatabase _mongoDatabase;
        private IMongoCollection<Building> BuildingCollection => _mongoDatabase.GetCollection<Building>(MongoCollectionName);

        public BuildingRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task Add(Building building, CancellationToken cancellationToken)
        {
            await BuildingCollection.InsertOneAsync(building, null, cancellationToken);
        }

        public async Task<IList<Building>> GetAll(CancellationToken cancellationToken)
        {
            var buildingCursor = await BuildingCollection.FindAsync(Builders<Building>.Filter.Empty);
            return await buildingCursor.ToListAsync();
        }

        public async Task<Building> Get(Guid id, CancellationToken cancellationToken)
        {
            var buildingCursor = await BuildingCollection.FindAsync(Builders<Building>.Filter.Where(building => building.Id == id));
            return await buildingCursor.FirstOrDefaultAsync();
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await BuildingCollection.DeleteOneAsync(Builders<Building>.Filter.Where(building => building.Id == id), cancellationToken);
        }

        public async Task Update(Building building, CancellationToken cancellationToken)
        {
            await BuildingCollection.ReplaceOneAsync(
                Builders<Building>.Filter.Where(buildingRecord => buildingRecord.Id == building.Id),
                building);
        }
    }
}