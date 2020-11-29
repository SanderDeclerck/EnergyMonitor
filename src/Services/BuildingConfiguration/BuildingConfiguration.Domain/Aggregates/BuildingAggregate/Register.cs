using System.Collections.Generic;
using Invoicing.Base.Ddd;

namespace BuildingConfiguration.Domain.Aggregates.BuildingAggregate
{
    public class Register : ValueObject
    {
        public Register(Tariff tariff)
        {
            Tariff = tariff;
        }

        public Tariff Tariff { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Tariff;
        }
    }
}