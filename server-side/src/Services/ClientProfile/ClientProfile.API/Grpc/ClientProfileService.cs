using ClientProfile.API.Infrastructure.Repositories;
using Grpc.Core;

namespace GrpcClientProfile
{
    public class ClientProfileService : ClientProfileGrpc.ClientProfileGrpcBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientProfileService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async override Task<ClientDTO> GetClientByUserId(GetClientByUserIdRequest request, ServerCallContext context)
        {
            var userId = Guid.Parse(request.UserId);
            var client = await _clientRepository.GetByUserIdAsync(userId);

            return new ClientDTO()
            {
                Id = client.Id.ToString(),
                FirstName = client.FirstName,
                LastName = client.LastName,
            };
        }

    }
}
