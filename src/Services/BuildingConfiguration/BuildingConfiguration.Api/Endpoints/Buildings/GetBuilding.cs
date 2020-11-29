using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using BuildingConfiguration.Domain.Aggregates.BuildingAggregate;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace BuildingConfiguration.Api.Endpoints.Buildings
{
    public class GetBuilding : BaseAsyncEndpoint<string, GetBuilding.Result>
    {
        private readonly IBuildingRepository _buildingRepository;

        public GetBuilding(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        [HttpGet(Routes.BuildingDetailUri)]
        [ProducesResponseType(typeof(Result), Status200OK)]
        [ProducesResponseType(typeof(string), Status400BadRequest)]
        [ProducesResponseType(typeof(string), Status404NotFound)]
        public async override Task<ActionResult<Result>> HandleAsync(string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var buildingGuid))
            {
                return BadRequest($"The id \"{id}\" could not be parsed.");
            }

            var building = await _buildingRepository.Get(buildingGuid, cancellationToken);

            if (building == null)
            {
                return NotFound($"The building with id \"{id}\" could not be found.");
            }

            return Ok(MapBuildingToResult(building));
        }

        private static Result MapBuildingToResult(Building building)
        {
            return new Result(building.Id.ToString(),
                building.Name,
                building.Location.PostalCode,
                building.Location.City,
                building.Location.Country,
                building.Meters.Select(meter =>
                    new Meter(meter.EanCode,
                        meter.MeterType,
                        meter.Registers.Select(register => new Register(register.Tariff)))));
        }

        public record Result(string Id, string Name, string PostalCode, string City, string Country, IEnumerable<Meter> meters);
        public record Meter(string EanCode, int meterType, IEnumerable<Register> registers);
        public record Register(int tariff);
    }
}