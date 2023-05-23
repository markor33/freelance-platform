using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;

namespace JobManagement.Application.Queries
{
    public class JobSearchFilters
    {
        public Guid? ProfessionId { get; set; } = null;
        public ExperienceLevel? ExperienceLevel { get; set; } = null;
        public PaymentType? PaymentType { get; set; } = null;

        public string ApplyFilters(string query)
        {
            if (ProfessionId is not null)
                query += @" AND j.""ProfessionId"" = @ProfessionId";
            if (ExperienceLevel is not null)
                query += @" AND j.""ExperienceLevel"" = @ExperienceLevel";
            if (PaymentType is not null)
                query += @" AND j.""Payment_Type"" = @PaymentType";

            return query;
        }
    }
}
