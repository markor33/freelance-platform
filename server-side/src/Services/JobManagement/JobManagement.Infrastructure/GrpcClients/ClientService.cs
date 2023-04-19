using GrpcClientProfile;
using JobManagement.Application.Services;

namespace JobManagement.Infrastructure.GrpcClients
{
    public class ClientService : IClientService
    {
        private readonly ClientProfileGrpc.ClientProfileGrpcClient _clientProfileClient;

        public ClientService(ClientProfileGrpc.ClientProfileGrpcClient clientProfileClient)
        {
            _clientProfileClient = clientProfileClient;
        }

        public async Task<Guid> GetClientIdByUserId(Guid userId)
        {
            var request = new GetClientByUserIdRequest()
            {
                UserId = userId.ToString()
            };
            
            var response = await _clientProfileClient.GetClientByUserIdAsync(request);

            return Guid.Parse(response.Id);
        }
    }
}
