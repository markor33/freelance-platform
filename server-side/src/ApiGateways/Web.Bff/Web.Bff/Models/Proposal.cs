using GrpcFreelancerProfile;
using GrpcJobManagement;

namespace Web.Bff.Models
{
    public class Proposal
    {
        public Guid Id { get; private set; }
        public string Text { get; private set; }
        public Payment Payment { get; private set; }
        public ProposalStatus Status { get; private set; }
        public FreelancerBasic Freelancer { get; private set; }
        public DateTime Created { get; private set; }

        public Proposal(ProposalDTO proposalDTO, FreelancerBasicData freelancerBasicData)
        {
            Id = Guid.Parse(proposalDTO.Id);
            Text = proposalDTO.Text;
            Payment = new Payment(proposalDTO.Payment);
            Status = (ProposalStatus)proposalDTO.Status;
            Freelancer = new FreelancerBasic(freelancerBasicData);
            Created = proposalDTO.Created.ToDateTime();
        }
    }

    public enum ProposalStatus
    {
        SENT,
        INTERVIEW,
        CLIENT_ACCEPTED,
        ACCEPTED
    }

    public class FreelancerBasic
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string TimeZoneId { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }

        public FreelancerBasic(FreelancerBasicData freelancerBasicData)
        {
            Id = Guid.Parse(freelancerBasicData.Id);
            FirstName = freelancerBasicData.FirstName;
            LastName = freelancerBasicData.LastName;
            TimeZoneId = freelancerBasicData.TimeZoneID;
            Country = freelancerBasicData.Country;
            City = freelancerBasicData.City;
        }
    }
}
