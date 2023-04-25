using FreelancerProfile.Domain.SeedWork;

namespace JobManagement.Domain.AggregatesModel.JobAggregate.Entities
{
    public class Skill : Entity<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid ProfessionId { get; private set; }
        public Profession Profession { get; private set; }
        public List<Job> Jobs { get; private set; }

        public Skill() { }

        public Skill(Guid id, Guid professionId, string name, string description)
        {
            Id = id;
            ProfessionId = professionId;
            Name = name;
            Description = description;
        }

    }
}
