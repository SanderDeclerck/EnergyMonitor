using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingConfiguration.Domain.Aggregates.BuildingAggregate;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace BuildingConfiguration.Api.Endpoints.Buildings
{
    public class DeleteBuilding : ControllerBase
    {
        private readonly IBuildingRepository _buildingRepository;

        public DeleteBuilding(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        [HttpDelete(Routes.DeleteBuildingUri)]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(typeof(string), Status400BadRequest)]
        public async Task<ActionResult> HandleAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var buildingGuid))
            {
                return BadRequest($"The id \"{id}\" could not be parsed.");
            }

            await _buildingRepository.Delete(buildingGuid, cancellationToken);

            return Ok();
        }
    }
}