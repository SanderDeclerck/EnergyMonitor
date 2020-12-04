using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingConfiguration.Domain.Aggregates.BuildingAggregate;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace BuildingConfiguration.Api.Endpoints.Meters
{
    public class CreateMeter : ControllerBase
    {
        private readonly IBuildingRepository _buildingRepository;

        public CreateMeter(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        [HttpPost(Routes.MeterUri)]
        [ProducesResponseType(typeof(Result), Status200OK)]
        [ProducesResponseType(typeof(string), Status400BadRequest)]
        [ProducesResponseType(typeof(string), Status404NotFound)]
        public async Task<ActionResult<Result>> HandleAsync([FromBody] Command command,
            [FromRoute] string buildingId,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(buildingId, out var buildingGuid) || command == null)
            {
                return BadRequest($"The id \"{buildingId}\" could not be parsed.");
            }

            var building = await _buildingRepository.Get(buildingGuid, cancellationToken);

            if (building == null)
            {
                return NotFound($"The building with id \"{buildingId}\" could not be found.");
            }

            building.AddMeter(new Meter(command.EanCode, Map(command.MeterType), command.HasOffPeakRegister));

            await _buildingRepository.Update(building, cancellationToken);

            return Ok(new Result());
        }

        private static BuildingConfiguration.Domain.Aggregates.BuildingAggregate.MeterType Map(int meterType)
        {
            return BuildingConfiguration.Domain.Aggregates.BuildingAggregate.MeterType.FromValue(meterType);
        }

        public record Command(string EanCode, int MeterType, bool HasOffPeakRegister);
        public record Result();
    }
}