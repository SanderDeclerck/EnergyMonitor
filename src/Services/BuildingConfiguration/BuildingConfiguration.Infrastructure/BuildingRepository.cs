using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BuildingConfiguration.Domain.Aggregates.BuildingAggregate;
using Invoicing.Base.Ddd;
using MediatR;
using MongoDB.Driver;

namespace BuildingConfiguration.Infrastructure
{
    public class BuildingRepository : IBuildingRepository
    {
        private const string MongoCollectionName = "Buildings";
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMediator _mediator;
        private IMongoCollection<Building> BuildingCollection => _mongoDatabase.GetCollection<Building>(MongoCollectionName);

        public BuildingRepository(IMongoDatabase mongoDatabase, IMediator mediator)
        {
            _mongoDatabase = mongoDatabase;
            _mediator = mediator;
        }

        public async Task Add(Building building, CancellationToken cancellationToken)
        {
            await BuildingCollection.InsertOneAsync(building, null, cancellationToken);
            await PublishDomainEvents(building);
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
            await PublishDomainEvents(building);
        }

        private async Task PublishDomainEvents(Entity entity)
        {
            foreach (var domainEvent in entity.DomainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
            entity.ClearDomainEvents();
        }
    }
}