using MediatR;
using FluentResults;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;

namespace JobManagement.Application.Commands
{
    [DataContract]
    public class CreateJobCommand : IRequest<Result<Job>>
    {
        [DataMember]
        public Guid ClientId { get; private set; }
        [DataMember]
        public string Title { get; private set; }
        [DataMember]
        public string Description { get; private set; }
        [DataMember]
        public ExperienceLevel ExperienceLevel { get; private set; }
        [DataMember]
        public Payment Payment { get; private set; }
        [DataMember]
        public List<Question> Questions { get; private set; }
        [DataMember]
        public Guid ProfessionId { get; private set; }
        [DataMember]
        public List<Guid> Skills { get; private set; }


        public CreateJobCommand() { }

        [JsonConstructor]
        public CreateJobCommand(Guid clientId, string title, string description, 
            ExperienceLevel experienceLevel, Payment payment, List<Question> questions, Guid professionId, List<Guid> skills)
        {
            ClientId = clientId;
            Title = title;
            Description = description;
            ExperienceLevel = experienceLevel;
            Payment = payment;
            Questions = questions;
            ProfessionId = professionId;
            Skills = skills;
        }
    }
}
