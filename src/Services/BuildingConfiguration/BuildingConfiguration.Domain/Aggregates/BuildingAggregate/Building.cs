using Invoicing.Base.Ddd;

namespace BuildingConfiguration.Domain.Aggregates.BuildingAggregate
{
    public class Building : Entity, IAggregateRoot
    {
        public Address Address { get; private set; }

        public Building(Address address)
        {
            Address = address;
        }
    }
}