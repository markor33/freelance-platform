using EventBus.Events;

namespace JobManagement.Application.Notifications
{
    public record ContractFinishedNotification : IntegrationEvent
    {
        public Guid ContractId { get; private set; }
        public Guid FreelancerId { get; private set; }
        public Guid JobId { get; private set; }
        public string JobTitle { get; private set; }

        public ContractFinishedNotification(Guid contractId, Guid freelancerId, Guid jobId, string jobTitle)
        {
            ContractId = contractId;
            FreelancerId = freelancerId;
            JobId = jobId;
            JobTitle = jobTitle;
        }
    }
}
