using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class UpdateProfileSummaryCommand : IRequest<Result<ProfileSummary>>
    {
        public Guid FreelancerId { get; private set; }
        public ProfileSummary ProfileSummary { get; private set; }

        public UpdateProfileSummaryCommand(Guid freelancerId, ProfileSummary profileSummary)
        {
            FreelancerId = freelancerId;
            ProfileSummary = profileSummary;
        }

    }
}
