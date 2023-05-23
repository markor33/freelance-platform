using FeedbackManagement.API.Persistence;
using Grpc.Core;
using GrpcFeedbackManagement;

namespace FeedbackManagement.API.GrpcServices
{
    public class FeedbackGrpcService : Feedback.FeedbackBase
    {
        private readonly IFinishedContractRepository _finishedContractRepository;

        public FeedbackGrpcService(IFinishedContractRepository finishedContractRepository)
        {
            _finishedContractRepository = finishedContractRepository;
        }

        public override async Task<GetFeedbackByContractResponse> GetFeedbackByContract(GetFeedbackByContractRequest request, ServerCallContext context)
        {
            var contract = await _finishedContractRepository.GetById(Guid.Parse(request.ContractId));
            if (contract is null)
                return new GetFeedbackByContractResponse();

            var response = new GetFeedbackByContractResponse();
            if (contract.ClientFeedback is not null)
                response.ClientFeedback = new FeedbackDTO() { Rating = contract.ClientFeedback.Rating, Text = contract.ClientFeedback.Text };
            if (contract.FreelancerFeedback is not null)
                response.FreelancerFeedback = new FeedbackDTO() { Rating = contract.FreelancerFeedback.Rating, Text = contract.FreelancerFeedback.Text };

            return response;
        }

        public override async Task<AverageRatingResponse> GetAverageClientRating(GetAverageClientRatingRequest request, ServerCallContext context)
        {
            var average = await _finishedContractRepository.GetClientAverageRating(Guid.Parse(request.ClientId));
            return new AverageRatingResponse() { AverageRating = average };
        }

        public override async Task<AverageRatingResponse> GetAverageFreelancerRating(GetAverageFreelancerRatingRequest request, ServerCallContext context)
        {
            var average = await _finishedContractRepository.GetFreelancerAverageRating(Guid.Parse(request.FreelancerId));
            return new AverageRatingResponse() { AverageRating = average };
        }

        public override async Task<GetFreelancerFeedbackResponse> GetFreelancerFeedback(GetFreelancerFeedbackRequest request, ServerCallContext context)
        {
            var contracts = await _finishedContractRepository.GetByFreelancer(Guid.Parse(request.FreelancerId));
            var response = new GetFreelancerFeedbackResponse();
            foreach (var contract in contracts)
                response.Feedbacks.Add(new FeedbackDTO()
                {
                    JobId = contract.JobId.ToString(),
                    Rating = contract.ClientFeedback.Rating,
                    Text = contract.ClientFeedback.Text
                });

            return response;
        }
    }
}
