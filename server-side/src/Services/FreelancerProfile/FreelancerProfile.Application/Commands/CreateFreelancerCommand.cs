using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    [DataContract]
    public class CreateFreelancerCommand : IRequest<Freelancer>
    {
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string FirstName { get; private set; }
        [DataMember]
        public string LastName { get; private set; }
        [DataMember]
        public string Country { get; private set; }
        [DataMember]
        public string City { get; private set; }
        [DataMember]
        public string Street { get; private set; }
        [DataMember]
        public string Number { get; private set; }
        [DataMember]
        public string ZipCode { get; private set; }
        [DataMember]
        public string PhoneNumber { get; private set; }
        [DataMember]
        public string TimeZoneId { get; private set; }

        public CreateFreelancerCommand() { }

        [JsonConstructor]
        public CreateFreelancerCommand(Guid userId, string firstName, string lastName, 
            string country, string city, string street, string number, string zipCode,
            string phoneNumber, string timeZoneId)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            City = city;
            Street = street;
            Number = number;
            ZipCode = zipCode;
            PhoneNumber = phoneNumber;
            TimeZoneId = timeZoneId;
        }

    }
}
