using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    [DataContract]
    public class AddEducationCommand : IRequest<Result<Education>>
    {
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string SchoolName { get; private set; }
        [DataMember]
        public string Degree { get; private set; }
        [DataMember]
        public DateTime Start { get; private set; }
        [DataMember]
        public DateTime End { get; private set; }

        public AddEducationCommand() { }

        [JsonConstructor]
        public AddEducationCommand(string schoolName, string degree, DateTime start, DateTime end)
        {
            SchoolName = schoolName;
            Degree = degree;
            Start = start;
            End = end;
        }

    }
}
