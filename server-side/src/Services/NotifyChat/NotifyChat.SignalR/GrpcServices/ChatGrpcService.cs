using Grpc.Core;
using GrpcNotifyChat;
using NotifyChat.SignalR.Persistence.Repositories;
using NotifyChat.SignalR.Services;

namespace NotifyChat.SignalR.GrpcServices
{
    public class ChatGrpcService : ChatService.ChatServiceBase
    {
        private readonly IChatRepository _chatRepository;
        private readonly IActiveUsersService _activeUsersService;

        public ChatGrpcService(
            IChatRepository chatRepository, 
            IActiveUsersService activeUsersService)
        {
            _chatRepository = chatRepository;
            _activeUsersService = activeUsersService;
        }

        public override async Task<GetChatsResponse> GetChats(GetChatsRequest request, ServerCallContext context)
        {
            var chats = await _chatRepository.GetByParticipant(Guid.Parse(request.UserId));
            var response = new GetChatsResponse();
            foreach (var chat in chats)
                response.Chats.Add(new ChatDTO()
                {
                    Id = chat.Id.ToString(),
                    JobId= chat.JobId.ToString(),
                    ProposalId = chat.ProposalId.ToString(),
                    ClientId = chat.ClientId.ToString(),
                    IsClientActive = _activeUsersService.IsActive(chat.ClientId),
                    FreelancerId = chat.FreelancerId.ToString(),
                    IsFreelancerActive = _activeUsersService.IsActive(chat.FreelancerId)
                });
            return response;
        }
    }
}
