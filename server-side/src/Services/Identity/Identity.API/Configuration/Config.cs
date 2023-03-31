using IdentityServer4.Models;

namespace Identity.API.Configuration
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("freelancer", "Freelancer Service"),
                new ApiResource("client", "Client Service"),
                new ApiResource("job", "Job Service")
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("freelancer", "Freelancer Service"),
                new ApiScope("client", "Client Service"),
                new ApiScope("job", "Job Service")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientId = "angular-app",
                    ClientName = "Angular.js SPA",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "freelancer", "client", "job"}
                }
            };
        }
    }
}
