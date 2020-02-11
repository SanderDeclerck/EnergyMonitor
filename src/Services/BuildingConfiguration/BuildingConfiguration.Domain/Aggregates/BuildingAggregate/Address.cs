using System.Collections.Generic;
using Invoicing.Base.Ddd;

namespace BuildingConfiguration.Domain.Aggregates.BuildingAggregate
{
    public class Address : ValueObject
    {
        public string Street { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string Country { get; }

        public Address(string street, string city, string postalCode, string country)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return PostalCode;
            yield return Country;
        }
    }
}