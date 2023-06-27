using ClientProfile.API.Infrastructure.Repositories;
using Grpc.Core;
using GrpcClientProfile;

namespace ClientProfile.API.Grpc
{
    public class ClientProfileGrpcService : GrpcClientProfile.ClientProfile.ClientProfileBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientProfileGrpcService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public override async Task<ClientBasicData> GetClientBasicDataById(GetClientBasicDataByIdRequest request, ServerCallContext context)
        {
            var client = await _clientRepository.GetById(Guid.Parse(request.Id));
            if (client is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Client not found"));

            return new ClientBasicData()
            {
                Id = client.Id.ToString(),
                FirstName = client.FirstName,
                LastName = client.LastName,
                TimeZoneID = client.Contact.TimeZoneId,
                Country = client.Contact.Address.Country,
                City = client.Contact.Address.City
            };
        }

        public async override Task<ClientBasicData> GetClientBasicDataByUserId(GetClientBasicDataByUserIdRequest request, ServerCallContext context)
        {
            var client = await _clientRepository.GetByUserIdAsync(Guid.Parse(request.UserId));
            if (client is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Client not found"));

            return new ClientBasicData()
            {
                Id = client.Id.ToString(),
                FirstName = client.FirstName,
                LastName = client.LastName,
                TimeZoneID = client.Contact.TimeZoneId,
                Country = client.Contact.Address.Country,
                City = client.Contact.Address.City
            };
        }

    }
}
