namespace Web.Bff.Models
{
    public class JobSearchFilters
    {
        public List<Guid> Professions { get; set; } = new List<Guid>();
        public List<ExperienceLevel> ExperienceLevels { get; set; } = new List<ExperienceLevel>();
        public List<PaymentType> PaymentTypes { get; set; } = new List<PaymentType>();
    }
}
