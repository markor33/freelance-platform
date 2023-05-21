using GrpcFreelancerProfile;
using GrpcJobManagement;

namespace Web.Bff.Services
{
    public class ContractService : IContractService
    {
        private readonly Contract.ContractClient _contractClient;
        private readonly FreelancerProfile.FreelancerProfileClient _freelancerProfileClient;

        public ContractService(
            Contract.ContractClient contractClient, 
            FreelancerProfile.FreelancerProfileClient freelancerProfileClient)
        {
            _contractClient = contractClient;
            _freelancerProfileClient = freelancerProfileClient;
        }

        public async Task<List<Models.Contract>> GetByClient(Guid clientId)
        {
            var response = await _contractClient.GetContractsByClientAsync(new GetContractsByClientRequest() { ClientId = clientId.ToString() });
            var contracts = new List<Models.Contract>();
            foreach (var contract in response.Contracts)
            {
                var freelancer = await _freelancerProfileClient
                    .GetFreelancerBasicDataByIdAsync(new GetFreelancerBasicDataByIdRequest() { Id = contract.FreelancerId.ToString()});
                contracts.Add(new Models.Contract(contract, freelancer));
            }

            return contracts;
        }

        public async Task<List<Models.Contract>> GetByJob(Guid jobId)
        {
            var response = await _contractClient.GetContractsByJobAsync(new GetContractsByJobRequest() { JobId = jobId.ToString() });
            var contracts = new List<Models.Contract>();
            foreach (var contract in response.Contracts)
            {
                var freelancer = await _freelancerProfileClient
                    .GetFreelancerBasicDataByIdAsync(new GetFreelancerBasicDataByIdRequest() { Id = contract.FreelancerId.ToString() });
                contracts.Add(new Models.Contract(contract, freelancer));
            }

            return contracts;
        }
    }
}
