using FreelancerProfile.Domain.SeedWork;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate
{
    public class Address : ValueObject
    {
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string ZipCode { get; private set; }

        public Address() { }

        public Address(string country, string city, string street, string number, string zipcode)
        {
            Country = country;
            City = city;
            Street = street;
            Number = number;
            ZipCode = zipcode;  
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Country;
            yield return City;
            yield return Street;
            yield return Number;
            yield return ZipCode;
        }
    }
}
