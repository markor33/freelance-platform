using System.Text.Json.Serialization;

namespace ClientProfile.API.Model
{
    public class Client
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Contact Contact { get; private set; } 

        public Client() { }

        [JsonConstructor]
        public Client(Guid userId, string firstName, string lastName, Contact contact)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Contact = contact;
        }

    }

    public class Contact
    {
        public string TimeZoneId { get; private set; }
        public Address Address { get; private set; }
        public string PhoneNumber { get; private set; }
        private TimeZoneInfo _timeZone = null;

        public TimeZoneInfo TimeZone
        {
            get
            {
                if (_timeZone is not null)
                    return _timeZone;
                _timeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
                return _timeZone;
            }
            private set { _timeZone = value; }
        }

        public Contact() { }

        [JsonConstructor]
        public Contact(string timeZoneId, Address address, string phoneNumber)
        {
            TimeZoneId = timeZoneId;
            TimeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
            Address = address;
            PhoneNumber = phoneNumber;
        }

    }

    public class Address
    {
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string ZipCode { get; private set; }

        public Address() { }

        [JsonConstructor]
        public Address(string country, string city, string street, string number, string zipCode)
        {
            Country = country;
            City = city;
            Street = street;
            Number = number;
            ZipCode = zipCode;
        }

    }

}
