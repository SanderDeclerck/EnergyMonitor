using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BuildingConfiguration.Domain.Events;
using Invoicing.Base.Ddd;
using NodaTime;

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

        public void AddReading(string meterEanCode, Tariff tariff, decimal value, IClock clock)
        {
            var meter = Meters.Single(meter => meter.EanCode == meterEanCode);
            var oldRegister = meter.Registers.Single(register => register.Tariff == tariff);
            var newRegister = oldRegister.WithNewReading(value, clock);
            meter.ReplaceRegister(oldRegister, newRegister);

            AddMeterReadingRegisteredDomainEvent(oldRegister, newRegister);
        }

        private void AddMeterReadingRegisteredDomainEvent(Register oldRegister, Register newRegister)
        {
            AddDomainEvent(new MeterReadingRegisteredDomainEvent(newRegister.LastReading!.Value,
                newRegister.LastReadingRegisteredOn!.Value,
                oldRegister.LastReading,
                oldRegister.LastReadingRegisteredOn));
        }
    }
}