using Bogus;
using BuildingConfiguration.Domain.Aggregates.BuildingAggregate;
using Xunit;

namespace BuildingConfiguration.UnitTests.Domain
{
    public class BuildingAggregateTest
    {
        Faker<Address> _addressFaker = new Faker<Address>()
            .CustomInstantiator(f => new Address(f.Address.StreetAddress(), f.Address.City(), f.Address.ZipCode(), f.Address.Country()));

        [Fact]
        public void NewBuilding_InitializesAddress()
        {
            var address = _addressFaker.Generate();

            var building = new Building(address);

            Assert.NotNull(building);
            Assert.NotNull(building.Address);
            Assert.Equal(address, building.Address);
        }
    }
}