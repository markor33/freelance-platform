using GrpcFeedbackManagement;
using GrpcFreelancerProfile;
using GrpcJobManagement;

namespace Web.Bff.Models
{
    public class Contract
    {
        public Guid Id { get; private init; }
        public Guid JobId { get; private init; }
        public string JobTitle { get; private init; }
        public Guid ClientId { get; private init; }
        public Guid FreelancerId { get; private init; }
        public string FreelancerName { get; private init; }
        public Payment Payment { get; set; }
        public DateTime Started { get; private init; }
        public DateTime? Finished { get; private init; }
        public ContractStatus Status { get; private init; }

        public Contract(ContractDTO contractDTO, FreelancerBasicData freelancer)
        {
            Id = Guid.Parse(contractDTO.Id);
            JobId= Guid.Parse(contractDTO.JobId);
            JobTitle= contractDTO.JobTitle;
            ClientId= Guid.Parse(contractDTO.ClientId);
            FreelancerId = Guid.Parse(contractDTO.FreelancerId);
            FreelancerName = freelancer.FirstName + " " + freelancer.LastName;
            Payment = new Payment(contractDTO.Payment);
            Started = contractDTO.Started.ToDateTime();
            if (contractDTO.Finished is not null) Finished = contractDTO.Finished.ToDateTime();
            Status = (ContractStatus)contractDTO.Status;
        }
    }

    public enum ContractStatus
    {
        ACTIVE,
        FINISHED,
        TERMINATED
    }
}
