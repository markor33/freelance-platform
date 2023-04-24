using AutoMapper;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;

namespace FreelancerProfile.Application.Queries
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Language, LanguageViewModel>();

            CreateMap<Skill, SkillViewModel>();

            CreateMap<Profession, ProfessionViewModel>();

            CreateMap<Education, EducationViewModel>();

            CreateMap<Employment, EmploymentViewModel>();

            CreateMap<Certification, CertificationViewModel>();

            CreateMap<Freelancer, FreelancerViewModel>();
        }
    }
}
