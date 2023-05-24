using GrpcClientProfile;
using GrpcJobManagement;

namespace Web.Bff.Models
{
    public class SearchJob
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public Payment Payment { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public int Credits { get; set; }
        public int NumOfProposals { get; set; }
        public int CurrentlyInterviewing { get; set; }
        public string ClientName { get; set; }
        public string ClientTimeZoneId { get; set; }
        public string ClientCountry { get; set; }
        public string ClientCity { get; set; }
        public float ClientAverageRating { get; set; }

        public SearchJob(JobDTO jobDTO, ClientBasicData clientBasicData, float clientAverageRating)
        {
            Id = Guid.Parse(jobDTO.Id);
            Title = jobDTO.Title;
            Description = jobDTO.Description;
            Created = jobDTO.Created.ToDateTime();
            Credits = jobDTO.Credits;
            Payment = new Models.Payment(jobDTO.Payment);
            ExperienceLevel = (ExperienceLevel)jobDTO.ExperienceLevel;
            NumOfProposals = jobDTO.NumOfProposals;
            CurrentlyInterviewing = jobDTO.CurrentlyInterviewing;
            ClientName = clientBasicData.FirstName + " " + clientBasicData.LastName;
            ClientTimeZoneId = clientBasicData.TimeZoneID;
            ClientCountry = clientBasicData.Country;
            ClientCity = clientBasicData.City;
            ClientAverageRating = clientAverageRating;
        }

    }
}
