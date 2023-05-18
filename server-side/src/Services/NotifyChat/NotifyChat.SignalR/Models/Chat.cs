using MongoDB.Bson.Serialization.Attributes;

namespace NotifyChat.SignalR.Models
{
    public class Chat
    {
        [BsonId]
        public Guid Id { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid FreelancerId { get; private set; }
        public Guid JobId { get; private set; }
        public Guid ProposalId { get; private set; }

        public Chat(Guid clientId, Guid freelancerId, Guid jobId, Guid proposalId)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            FreelancerId = freelancerId;
            JobId = jobId;
            ProposalId = proposalId;
        }
    }
}
