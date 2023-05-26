using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    public class UpdateEducationCommand : IRequest<Result<Education>>
    {
        public Guid FreelancerId { get; set; }
        public Guid EducationId { get; private set; }
        public string SchoolName { get; private set; }
        public string Degree { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        [JsonConstructor]
        public UpdateEducationCommand(Guid educationId, string schoolName, string degree, DateTime start, DateTime end)
        {
            EducationId = educationId;
            SchoolName = schoolName;
            Degree = degree;
            Start = start;
            End = end;
        }
    }
}
