using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BuildingConfiguration.Domain.Aggregates.BuildingAggregate;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace BuildingConfiguration.Api.Endpoints.Meters
{
    public class RegisterReadings : ControllerBase
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IClock _clock;

        public RegisterReadings(IBuildingRepository buildingRepository, IClock clock)
        {
            _buildingRepository = buildingRepository;
            _clock = clock;
        }

        [HttpPost(Routes.RegisterReadingsUri)]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(typeof(string), Status400BadRequest)]
        [ProducesResponseType(typeof(string), Status404NotFound)]
        public async Task<ActionResult> HandleAsync([FromRoute] string buildingId,
        [FromRoute] string meterEanCode,
        [FromBody] Command command,
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

            foreach (var reading in command.Readings)
            {
                var tariff = Tariff.FromValue(reading.Tariff);
                building.AddReading(meterEanCode, tariff, reading.Value, _clock);
            }

            await _buildingRepository.Update(building, cancellationToken);

            return Ok();
        }

        public record Command(IEnumerable<Reading> Readings);
        public record Reading(int Tariff, decimal Value);
    }
}