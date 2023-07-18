using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.SeedWork;
using MediatR;

namespace FreelancerProfile.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehavior(IFreelancerRepository freelancerRepository)
        {
            _unitOfWork = freelancerRepository.UnitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_unitOfWork.HasActiveTransaction)
            {
                return await next();
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
            }
            catch (Exception ex)
            {
                var a = 1;
            }
            var response = await next();
            await _unitOfWork.CommitTransactionAsync();

            return response;
        }
    }
}
