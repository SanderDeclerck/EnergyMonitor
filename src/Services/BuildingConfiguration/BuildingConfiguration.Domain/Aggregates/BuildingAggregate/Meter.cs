using System.Collections.Generic;
using System.Collections.Immutable;
using Invoicing.Base.Ddd;

namespace BuildingConfiguration.Domain.Aggregates.BuildingAggregate
{
    public class Meter : Entity
    {
        public Meter(string eanCode, MeterType meterType, IEnumerable<Register> registers)
        {
            EanCode = eanCode;
            MeterType = meterType;
            Registers = ImmutableList.CreateRange(registers);
        }

        public Meter(string eanCode, MeterType meterType, bool hasOffPeakRegister)
        {
            EanCode = eanCode;
            MeterType = meterType;

            if (hasOffPeakRegister)
            {
                Registers = ImmutableList.CreateRange(new[] { new Register(Tariff.Peek), new Register(Tariff.OffPeek) });
            }
            else
            {
                Registers = ImmutableList.CreateRange(new[] { new Register(Tariff.AllDay) });
            }
        }

        public string EanCode { get; private set; }
        public MeterType MeterType { get; private set; }
        public ImmutableList<Register> Registers { get; private set; }
    }
}