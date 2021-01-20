using Ardalis.SmartEnum;

namespace BuildingConfiguration.Domain.Aggregates.BuildingAggregate
{
    public class Tariff : SmartEnum<Tariff>
    {
        public static readonly Tariff Peek = new Tariff(nameof(Peek), 1);
        public static readonly Tariff OffPeek = new Tariff(nameof(OffPeek), 2);
        public static readonly Tariff AllDay = new Tariff(nameof(AllDay), 3);

        public Tariff(string name, int value) : base(name, value)
        {
        }
    }
}