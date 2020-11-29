using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Invoicing.Base.Ddd;

namespace BuildingConfiguration.Domain.Aggregates.BuildingAggregate
{
    public class Building : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public BuildingLocation Location { get; private set; }
        public ImmutableList<Meter> Meters { get; private set; }

        public Building(string name, BuildingLocation address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = address;
            Meters = ImmutableList<Meter>.Empty;
        }
        public Building(string name, BuildingLocation address, IEnumerable<Meter> meters)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = address;
            Meters = ImmutableList.CreateRange(meters);
        }

        public void AddMeter(Meter meter)
        {
            Meters = Meters.Add(meter);
        }
    }
}