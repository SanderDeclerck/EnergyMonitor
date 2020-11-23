using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using BuildingConfiguration.Domain.Aggregates.BuildingAggregate;
using Microsoft.AspNetCore.Mvc;

namespace BuildingConfiguration.Api.Endpoints.Buildings
{
    public class ListBuildings : BaseAsyncEndpoint<ListBuildings.Result>
    {
        private readonly IBuildingRepository _buildingRepository;

        public ListBuildings(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        [HttpGet(Routes.BuildingUri)]
        public async override Task<ActionResult<Result>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var buildings = await _buildingRepository.GetAll(cancellationToken);

            var buildingRecords = buildings.Select(building => new BuildingRecord(building.Id.ToString(), building.Name));

            return Ok(new Result(buildingRecords));
        }

        public record Result(IEnumerable<BuildingRecord> buildings);
        public record BuildingRecord(string Id, string Name);
    }
}