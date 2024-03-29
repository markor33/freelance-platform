﻿using FluentResults;
using FreelancerProfile.Domain.Repositories;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class DeleteCertificationCommandHandler : IRequestHandler<DeleteCertificationCommand, Result>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public DeleteCertificationCommandHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Result> Handle(DeleteCertificationCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            freelancer.DeleteCertification(request.CertificationId);

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync();
            if (!result)
                return Result.Fail("Delete certification action failed");

            return Result.Ok();
        }
    }
}
