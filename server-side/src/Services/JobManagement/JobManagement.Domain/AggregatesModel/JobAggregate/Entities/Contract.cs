using FreelancerProfile.Domain.SeedWork;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;

namespace JobManagement.Domain.AggregatesModel.JobAggregate.Entities
{
    public class Contract : Entity<Guid>
    {
        public Guid JobId { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid FreelancerId { get; private set; }
        public Payment Payment { get; private set; }
        public DateTime Date { get; private set; }

        public Contract() { }

        public Contract(Guid jobId, Guid clientId, Guid freelancerId, Payment payment)
        {
            JobId = jobId;
            ClientId = clientId;
            FreelancerId = freelancerId;
            Payment = payment;
            Date = DateTime.UtcNow;
        }
    }
}
