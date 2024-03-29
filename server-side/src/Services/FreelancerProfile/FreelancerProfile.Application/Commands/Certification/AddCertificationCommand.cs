﻿using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    public class AddCertificationCommand : IRequest<Result<Certification>>
    {
        public Guid FreelancerId { get; set; }
        public string Name { get; private set; }
        public string Provider { get; private set; }
        public string? Description { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        [JsonConstructor]
        public AddCertificationCommand(string name, string provider, string description, DateTime start, DateTime end)
        {
            Name = name;
            Provider = provider;
            if (string.IsNullOrEmpty(description)) description = null;
            Description = description;
            Start = start;
            End = end;
        }

    }
}
