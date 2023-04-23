using FluentResults;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JobManagement.Application.Commands
{
    [DataContract]
    public class CreateProposalCommand : IRequest<Result<Proposal>>
    {
        [DataMember]
        public Guid FreelancerId { get; private set; }
        [DataMember]
        public Guid JobId { get; private set; }
        [DataMember]
        public string Text { get; private set; }
        [DataMember]
        public Payment Payment { get; private set; }
        [DataMember]
        public List<Answer> Answers { get; private set; }

        public CreateProposalCommand() { }

        [JsonConstructor]
        public CreateProposalCommand(Guid freelancerId, Guid jobId, string text, Payment payment, List<Answer> answers)
        {
            FreelancerId = freelancerId;
            JobId = jobId;
            Text = text;
            Payment = payment;
            Answers = answers;
        }

    }
}
