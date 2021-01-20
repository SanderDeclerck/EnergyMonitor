using Ardalis.SmartEnum;

namespace BuildingConfiguration.Domain.Aggregates.BuildingAggregate
{
    public class MeterType : SmartEnum<MeterType>
    {
        public static readonly MeterType Electricity = new MeterType(nameof(Electricity), 1);
        public static readonly MeterType Water = new MeterType(nameof(Water), 2);
        public static readonly MeterType Gas = new MeterType(nameof(Gas), 3);

        public MeterType(string name, int value) : base(name, value)
        {
        }
    }
}