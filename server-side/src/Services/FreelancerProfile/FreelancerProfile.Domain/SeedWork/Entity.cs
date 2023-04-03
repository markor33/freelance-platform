namespace FreelancerProfile.Domain.SeedWork
{
    public abstract class Entity<TId>
    {
        private TId _id;

        public virtual TId Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        public override bool Equals(object? obj)
        {
            var entity = obj as Entity<TId>;
            if (entity != null)
            {
                return this.Equals(entity);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

    }
}
