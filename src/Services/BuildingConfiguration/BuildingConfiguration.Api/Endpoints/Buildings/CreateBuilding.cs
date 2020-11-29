using System.Threading;
using System.Threading.Tasks;
using BuildingConfiguration.Domain.Aggregates.BuildingAggregate;
using Microsoft.AspNetCore.Mvc;

namespace BuildingConfiguration.Api.Endpoints.Buildings
{
    public class CreateBuilding : ControllerBase
    {
        private readonly IBuildingRepository _buildingRespository;

        public CreateBuilding(IBuildingRepository buildingRespository)
        {
            _buildingRespository = buildingRespository;
        }

        [HttpPost(Routes.BuildingUri)]
        public async Task<ActionResult<Result>> HandleAsync([FromBody] Command request, CancellationToken cancellationToken = default)
        {
            var buildingLocation = new BuildingLocation(request.City, request.Postalcode, request.Country);
            var building = new Building(request.Name, buildingLocation);

            await _buildingRespository.Add(building, cancellationToken);

            return Ok(new Result(building.Id.ToString()));
        }

        public record Command(string Name, string Postalcode, string City, string Country);
        public record Result(string Id);
    }
}