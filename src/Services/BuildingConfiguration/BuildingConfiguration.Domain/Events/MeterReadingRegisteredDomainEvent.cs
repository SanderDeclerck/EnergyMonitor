using MediatR;
using NodaTime;

namespace BuildingConfiguration.Domain.Events
{
    public class MeterReadingRegisteredDomainEvent : INotification
    {

        public MeterReadingRegisteredDomainEvent(decimal newReading, Instant newReadingRegisteredOn, decimal? oldReading, Instant? oldReadingRegisteredOn)
        {
            NewReading = newReading;
            NewReadingRegisteredOn = newReadingRegisteredOn;
            OldReading = oldReading;
            OldReadingRegisteredOn = oldReadingRegisteredOn;
        }

        public decimal NewReading { get; set; }
        public Instant NewReadingRegisteredOn { get; set; }
        public decimal? OldReading { get; set; }
        public Instant? OldReadingRegisteredOn { get; set; }
    }
}