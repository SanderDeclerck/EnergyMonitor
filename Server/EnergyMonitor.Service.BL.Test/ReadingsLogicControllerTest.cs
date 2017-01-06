using System;
using EnergyMonitor.Service.Dao;
using EnergyMonitor.Service.Model;
using Moq;
using Xunit;

namespace EnergyMonitor.Service.BL.Test
{
    public class ReadingsLogicControllerTest
    {
        [Fact]
        public void InsertReading_WhenCalled_PassesToDao()
        {
            var readingsDaoMock = new Mock<IReadingsDao>();
            var subject = new ReadingsLogicController(readingsDaoMock.Object);
            var reading = new EnergyReading
            {
                Timestamp = new DateTime(2016, 1, 1, 0, 0, 0),
                Unit = EnergyUnit.Kwh,
                Type = EnergyType.Electricity,
                Value = 500
            };
            
            subject.InsertReading(reading);

            readingsDaoMock.Verify(m => m.InsertReading(It.IsAny<EnergyReading>()));
        }
    }
}