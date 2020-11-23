using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingConfiguration.Domain.Aggregates.BuildingAggregate
{
    public interface IBuildingRepository
    {
         Task Add(Building building, CancellationToken cancellationToken);
         Task<IList<Building>> GetAll(CancellationToken cancellationToken);
         Task<Building> Get(Guid id, CancellationToken cancellationToken);
    }
}