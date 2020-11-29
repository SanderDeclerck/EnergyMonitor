using System.Collections.Generic;
using Invoicing.Base.Ddd;

namespace BuildingConfiguration.Domain.Aggregates.BuildingAggregate
{
    public class BuildingLocation : ValueObject
    {
        public string City { get; }
        public string PostalCode { get; }
        public string Country { get; }

        public BuildingLocation(string city, string postalCode, string country)
        {
            City = city;
            PostalCode = postalCode;
            Country = country;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return City;
            yield return PostalCode;
            yield return Country;
        }
    }
}