

namespace Web.Bff.Models
{
    public class JobSearchFilters
    {
        public Guid? ProfessionId { get; set; } = null;
        public ExperienceLevel? ExperienceLevel { get; set; } = null;
        public PaymentType? PaymentType { get; set; } = null;
    }
}
