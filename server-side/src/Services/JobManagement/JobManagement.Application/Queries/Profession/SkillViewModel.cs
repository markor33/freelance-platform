namespace JobManagement.Application.Queries
{
    public class SkillViewModel : IEquatable<SkillViewModel>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals((SkillViewModel)obj);
        }

        public bool Equals(SkillViewModel? other)
        {
            if (other == null) return false;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}
