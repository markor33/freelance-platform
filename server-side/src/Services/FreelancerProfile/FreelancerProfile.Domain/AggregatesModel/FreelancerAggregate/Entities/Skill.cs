using FreelancerProfile.Domain.SeedWork;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities
{
    public class Skill : Entity<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid ProfessionId { get; private set; }
        public Profession Profession { get; private set; }
        public List<Freelancer> Freelancers { get; private set; }

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
