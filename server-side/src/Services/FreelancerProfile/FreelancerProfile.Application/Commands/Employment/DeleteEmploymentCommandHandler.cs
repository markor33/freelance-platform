﻿using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class DeleteEmploymentCommandHandler : IRequestHandler<DeleteEmploymentCommand, Result>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public DeleteEmploymentCommandHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Result> Handle(DeleteEmploymentCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            var deleteResult = freelancer.DeleteEmployment(request.EmploymentId);
            if (deleteResult.IsFailed)
                return deleteResult;

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync();
            if (!result)
                return Result.Fail("Delete employment action failed");

            return Result.Ok();
        }
    }
}