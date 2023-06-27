using FreelancerProfile.Domain.SeedWork;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using MediatR;

namespace JobManagement.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehavior(IJobRepository freelancerRepository)
        {
            _unitOfWork = freelancerRepository.UnitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_unitOfWork.HasActiveTransaction)
            {
                return await next();
            }

            await _unitOfWork.BeginTransactionAsync();
            var response = await next();
            await _unitOfWork.CommitTransactionAsync();

            return response;
        }
    }
}
