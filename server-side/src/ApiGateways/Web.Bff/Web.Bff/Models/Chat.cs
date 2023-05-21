namespace Web.Bff.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public Guid ProposalId { get; set; }
        public Guid? ContractId { get; set; } = null;
        public Guid ClientId { get; set; }
        public bool IsClientActive { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public Guid FreelancerId { get; set; }
        public bool IsFreelancerActive { get; set; }
        public string FreelancerName { get; set; } = string.Empty;
    }
}
