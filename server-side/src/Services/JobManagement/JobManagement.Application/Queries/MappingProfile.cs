using AutoMapper;
using JobManagement.Domain.AggregatesModel.JobAggregate;

namespace JobManagement.Application.Queries
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Job, JobViewModel>();
        }

    }
}
