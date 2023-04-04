using AutoMapper;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;

namespace FreelancerProfile.Application.Queries
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressViewModel>();

            CreateMap<Contact, ContactViewModel>();

            CreateMap<Freelancer, FreelancerViewModel>();
        }
    }
}
