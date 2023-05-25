using FluentResults;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JobManagement.Application.Commands.JobCommands
{
    [DataContract]
    public class EditJobCommand : IRequest<Result<Job>>
    {
        [DataMember]
        public Guid JobId { get; set; }
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

        public EditJobCommand() { }

        [JsonConstructor]
        public EditJobCommand(Guid jobId, string title, string description, ExperienceLevel experienceLevel, 
            Payment payment, List<Question> questions, Guid professionId, List<Guid> skills)
        {
            JobId = jobId;
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
