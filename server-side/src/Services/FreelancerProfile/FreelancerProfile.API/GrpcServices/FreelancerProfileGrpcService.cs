using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using Grpc.Core;
using GrpcFreelancerProfile;

namespace FreelancerProfile.API.GrpcServices
{
    public class FreelancerProfileGrpcService : GrpcFreelancerProfile.FreelancerProfile.FreelancerProfileBase
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public FreelancerProfileGrpcService(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public override async Task<FreelancerBasicData> GetFreelancerBasicDataById(GetFreelancerBasicDataByIdRequest request, ServerCallContext context)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(Guid.Parse(request.Id));
            return ConvertToFreelancerBasicData(freelancer);
        }

        public async override Task<FreelancerBasicData> GetFreelancerBasicDataByUserId(GetFreelancerBasicDataByUserIdRequest request, ServerCallContext context)
        {
            var freelancer = await _freelancerRepository.GetByUserIdAsync(Guid.Parse(request.UserId));
            return ConvertToFreelancerBasicData(freelancer);
        }

        private static FreelancerBasicData ConvertToFreelancerBasicData(Freelancer freelancer)
        {
            if (freelancer is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Freelancer not found"));

            return new FreelancerBasicData()
            {
                Id = freelancer.Id.ToString(),
                FirstName = freelancer.FirstName,
                LastName = freelancer.LastName,
                ExperienceLevel = (int)freelancer.ExperienceLevel,
                ProfessionId = freelancer.ProfessionId.ToString() ?? string.Empty,
                TimeZoneID = freelancer.Contact.TimeZoneId,
                Country = freelancer.Contact.Address.Country,
                City = freelancer.Contact.Address.City
            };
        }
    }
}
