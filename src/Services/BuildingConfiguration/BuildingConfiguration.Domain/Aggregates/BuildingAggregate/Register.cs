using System;
using System.Collections.Generic;
using Invoicing.Base.Ddd;
using NodaTime;

namespace BuildingConfiguration.Domain.Aggregates.BuildingAggregate
{
    public class Register : ValueObject
    {
        public Register(Tariff tariff)
        {
            Tariff = tariff;
        }

        public Tariff Tariff { get; private set; }
        public decimal? LastReading { get; private set; }
        public Instant? LastReadingRegisteredOn { get; private set; }

        public void RegisterReading(decimal value, IClock clock)
        {
            if (value < LastReading)
            {
                throw new ArgumentException($"The value of a new meter reading cannot be less than the value of the previous reading.", nameof(value));
            }

            LastReading = value;
            LastReadingRegisteredOn = clock.GetCurrentInstant();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Tariff;
        }
    }
}