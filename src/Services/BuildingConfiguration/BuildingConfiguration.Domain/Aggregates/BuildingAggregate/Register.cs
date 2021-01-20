using System;
using System.Collections.Generic;
using Invoicing.Base.Ddd;
using NodaTime;

namespace BuildingConfiguration.Domain.Aggregates.BuildingAggregate
{
    public class Register : ValueObject
    {
        public Register(Tariff tariff, decimal? lastReading, Instant? lastReadingRegisteredOn)
        {
            Tariff = tariff;
            LastReading = lastReading;
            LastReadingRegisteredOn = lastReadingRegisteredOn;
        }

        public Tariff Tariff { get; }
        public decimal? LastReading { get; }
        public Instant? LastReadingRegisteredOn { get; }

        public Register WithNewReading(decimal value, IClock clock)
        {
            if (value < LastReading)
            {
                throw new ArgumentException($"The value of a new meter reading cannot be less than the value of the previous reading.", nameof(value));
            }

            return new Register(Tariff, value, clock.GetCurrentInstant());
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Tariff;
        }
    }
}