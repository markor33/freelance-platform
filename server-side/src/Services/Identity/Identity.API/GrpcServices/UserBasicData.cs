namespace Identity.API.GrpcServices
{
    public class UserBasicData
    {
        public Guid DomainId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public UserBasicData(Guid domainId, string firstName, string lastName)
        {
            DomainId = domainId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
