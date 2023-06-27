namespace NotifyChat.SignalR.Models
{
    public class ProposalSubmittedNotificationData
    {
        public Guid JobId { get; private set; }
        public string JobName { get; private set; }


        public ProposalSubmittedNotificationData() { }

        public ProposalSubmittedNotificationData(Guid jobId, string jobName)
        {
            JobId = jobId;
            JobName = jobName;
        }
    }
}
