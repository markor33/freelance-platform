using Xunit;

namespace FreelancerProfile.IntegrationTests.Setup
{
    public class BaseIntegrationTest : IClassFixture<TestDatabaseFactory>
    {
        protected TestDatabaseFactory Factory { get; }

        public BaseIntegrationTest(TestDatabaseFactory factory)
        {
            Factory = factory;
        }
    }
}
