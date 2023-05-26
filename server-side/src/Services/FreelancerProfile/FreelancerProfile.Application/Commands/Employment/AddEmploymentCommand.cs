using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    public class AddEmploymentCommand : IRequest<Result<Employment>>
    {
        public Guid FreelancerId { get; set; }
        public string Company { get; private set; }
        public string Title { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public string? Description { get; private set; }

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
