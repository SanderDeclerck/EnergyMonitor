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
        public async Task<ActionResult<Result>> HandleAsync([FromRoute] string buildingId,
            [FromBody] Command command,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(buildingId, out var buildingGuid))
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

        private BuildingConfiguration.Domain.Aggregates.BuildingAggregate.MeterType Map(MeterType meterType)
        {
            return BuildingConfiguration.Domain.Aggregates.BuildingAggregate.MeterType.FromValue((int)meterType);
        }

        public record Command(string EanCode, MeterType MeterType, bool HasOffPeakRegister);
        public record Result();
        public enum MeterType
        {
            Electricity = 1,
            Water = 2,
            Gas = 3
        }
    }
}