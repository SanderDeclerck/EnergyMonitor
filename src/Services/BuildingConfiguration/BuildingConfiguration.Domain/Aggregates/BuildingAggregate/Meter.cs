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
                Registers = ImmutableList.CreateRange(new[] { new Register(Tariff.Peek, null, null), new Register(Tariff.OffPeek, null, null) });
            }
            else
            {
                Registers = ImmutableList.CreateRange(new[] { new Register(Tariff.AllDay, null, null) });
            }
        }

        internal void ReplaceRegister(Register oldRegister, Register newRegister)
        {
            Registers = Registers.Replace(oldRegister, newRegister);
        }

        public string EanCode { get; private set; }
        public MeterType MeterType { get; private set; }
        public ImmutableList<Register> Registers { get; private set; }
    }
}