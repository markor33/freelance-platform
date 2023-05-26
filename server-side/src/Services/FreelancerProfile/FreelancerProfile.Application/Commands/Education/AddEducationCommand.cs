    using FluentResults;
    using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
    using MediatR;
    using System.Text.Json.Serialization;

    namespace FreelancerProfile.Application.Commands
    {
        public class AddEducationCommand : IRequest<Result<Education>>
        {
            public Guid FreelancerId { get; set; }
            public string SchoolName { get; private set; }
            public string Degree { get; private set; }
            public DateTime Start { get; private set; }
            public DateTime End { get; private set; }

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
