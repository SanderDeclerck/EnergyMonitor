using System;
using Invoicing.Base.Ddd;

namespace BuildingConfiguration.Domain.Aggregates.BuildingAggregate
{
    public class Building : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public BuildingLocation Location { get; private set; }

        public Building(string name, BuildingLocation address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = address;
        }
    }
}