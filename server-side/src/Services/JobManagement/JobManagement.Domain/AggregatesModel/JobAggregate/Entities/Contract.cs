using FreelancerProfile.Domain.SeedWork;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;

namespace JobManagement.Domain.AggregatesModel.JobAggregate.Entities
{
    public class Contract : Entity<Guid>
    {
        public Guid ClientId { get; private set; }
        public Guid FreelancerId { get; private set; }
        public Payment Payment { get; private set; }
        public DateTime Started { get; private set; }
        public DateTime? Finished { get; private set; }
        public ContractStatus Status { get; private set; }

        public Contract() { }

        public Contract(Guid clientId, Guid freelancerId, Payment payment)
        {
            ClientId = clientId;
            FreelancerId = freelancerId;
            Payment = payment;
            Started = DateTime.UtcNow;
            Finished = null;
            Status = ContractStatus.ACTIVE;
        }

        public void ChangeStatus(ContractStatus status)
        {
            if (status == ContractStatus.FINISHED)
                Finished = DateTime.UtcNow;
            Status = status;
        }

    }
}
