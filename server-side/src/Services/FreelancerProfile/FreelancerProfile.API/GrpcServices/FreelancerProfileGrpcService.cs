using FreelancerProfile.Application.Queries;
using Grpc.Core;
using GrpcFreelancerProfile;

namespace FreelancerProfile.API.GrpcServices
{
    public class FreelancerProfileGrpcService : GrpcFreelancerProfile.FreelancerProfile.FreelancerProfileBase
    {
        private readonly IFreelancerQueries _queries;

        public FreelancerProfileGrpcService(IFreelancerQueries freelancerQueries)
        {
            _queries = freelancerQueries;
        }

        public override async Task<FreelancerBasicData> GetFreelancerBasicDataById(GetFreelancerBasicDataByIdRequest request, ServerCallContext context)
        {
            var result = await _queries.GetByIdAsync(Guid.Parse(request.Id));
            if (result.IsFailed)
                throw new RpcException(new Status(StatusCode.NotFound, "Freelancer not found"));

            var freelancer = result.Value;
            return new FreelancerBasicData()
            {
                Id = freelancer.Id.ToString(),
                FirstName = freelancer.FirstName,
                LastName = freelancer.LastName,
                ExperienceLevel = (int)freelancer.ExperienceLevel,
                ProfessionId = freelancer.Profession.Id.ToString(),
                TimeZoneID = freelancer.Contact.TimeZoneId,
                Country = freelancer.Contact.Address.Country,
                City = freelancer.Contact.Address.City
            };
        }

        public async override Task<FreelancerBasicData> GetFreelancerBasicDataByUserId(GetFreelancerBasicDataByUserIdRequest request, ServerCallContext context)
        {
            var result = await _queries.GetByUserIdAsync(Guid.Parse(request.UserId));
            if (result.IsFailed)
                throw new RpcException(new Status(StatusCode.NotFound, "Freelancer not found"));

            var freelancer = result.Value;
            return new FreelancerBasicData()
            {
                Id = freelancer.Id.ToString(),
                FirstName = freelancer.FirstName,
                LastName = freelancer.LastName,
                ExperienceLevel = (int)freelancer.ExperienceLevel,
                ProfessionId = freelancer.Profession.Id.ToString(),
                TimeZoneID = freelancer.Contact.TimeZoneId,
                Country = freelancer.Contact.Address.Country,
                City = freelancer.Contact.Address.City
            };
        }
    }
}
