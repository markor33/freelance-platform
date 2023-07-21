using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.Repositories;
using Grpc.Core;
using GrpcFreelancerProfile;

namespace FreelancerProfile.API.GrpcServices
{
    public class FreelancerProfileGrpcService : GrpcFreelancerProfile.FreelancerProfile.FreelancerProfileBase
    {
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly IFreelancerQueries _freelancerQueries;

        public FreelancerProfileGrpcService(
            IFreelancerRepository freelancerRepository, 
            IFreelancerQueries freelancerQueries)
        {
            _freelancerQueries = freelancerQueries;
        }


        public override async Task<FreelancerBasicData> GetFreelancerBasicDataById(GetFreelancerBasicDataByIdRequest request, ServerCallContext context)
        {
            var freelancer = await _freelancerQueries.GetByIdAsync(Guid.Parse(request.Id));
            return ConvertToFreelancerBasicData(freelancer.Value);
        }

        public async override Task<FreelancerBasicData> GetFreelancerBasicDataByUserId(GetFreelancerBasicDataByUserIdRequest request, ServerCallContext context)
        {
            var freelancer = await _freelancerQueries.GetByUserIdAsync(Guid.Parse(request.UserId));
            return ConvertToFreelancerBasicData(freelancer.Value);
        }

        private static FreelancerBasicData ConvertToFreelancerBasicData(FreelancerViewModel freelancer)
        {
            if (freelancer is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Freelancer not found"));

            return new FreelancerBasicData()
            {
                Id = freelancer.Id.ToString(),
                FirstName = freelancer.FirstName,
                LastName = freelancer.LastName,
                ExperienceLevel = (int)freelancer.ExperienceLevel,
                ProfessionId = freelancer.Profession?.Id.ToString() ?? string.Empty,
                TimeZoneID = freelancer.Contact.TimeZoneId,
                Country = freelancer.Contact.Address.Country,
                City = freelancer.Contact.Address.City
            };
        }
    }
}
