using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    [DataContract]
    public class AddEmploymentCommand : IRequest<Employment>
    {
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string Company { get; private set; }
        [DataMember]
        public string Title { get; private set; }
        [DataMember]
        public DateTime Start { get; private set; }
        [DataMember]
        public DateTime End { get; private set; }
        [DataMember]
        public string? Description { get; private set; }

        public AddEmploymentCommand() { }

        [JsonConstructor]
        public AddEmploymentCommand(string company, string title, DateTime start, DateTime end, string? description)
        {
            Company = company;
            Title = title;
            Start = start;
            End = end;
            if (string.IsNullOrEmpty(description)) description = null;
            Description = description;
        }

    }
}
