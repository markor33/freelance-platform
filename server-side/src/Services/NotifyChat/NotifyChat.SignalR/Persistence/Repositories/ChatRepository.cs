﻿using MongoDB.Driver;
using NotifyChat.SignalR.Models;
using NotifyChat.SignalR.Persistence.Settings;

namespace NotifyChat.SignalR.Persistence.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly IMongoCollection<Chat> _chatCollection;
        
        public ChatRepository(IMongoDbFactory mongoDb)
        {
            _chatCollection = mongoDb.GetCollection<Chat>("chats");
        }

        public async Task<Chat> GetById(Guid id)
        {
            var filter = Builders<Chat>.Filter.Eq(c => c.Id, id);
            return await (await _chatCollection.FindAsync<Chat>(filter)).FirstOrDefaultAsync();
        }

        public async Task<List<Chat>> GetByParticipant(Guid participantId)
        {
            var filter = Builders<Chat>.Filter.Eq(c => c.ClientId, participantId);
            filter |= Builders<Chat>.Filter.Eq(c => c.FreelancerId, participantId);
            return await (await _chatCollection.FindAsync<Chat>(filter)).ToListAsync();
        }

        public async Task<Chat> GetByClient(Guid clientId)
        {
            var filter = Builders<Chat>.Filter.Eq(c => c.ClientId, clientId);
            return await(await _chatCollection.FindAsync<Chat>(filter)).FirstOrDefaultAsync();
        }

        public async Task<Chat> GetByFreelancer(Guid freelancerId)
        {
            var filter = Builders<Chat>.Filter.Eq(c => c.FreelancerId, freelancerId);
            return await(await _chatCollection.FindAsync<Chat>(filter)).FirstOrDefaultAsync();
        }

        public async Task<Chat> GetByProposal(Guid proposalId)
        {
            var filter = Builders<Chat>.Filter.Eq(c => c.ProposalId, proposalId);
            return await(await _chatCollection.FindAsync<Chat>(filter)).FirstOrDefaultAsync();
        }

        public async Task Create(Chat chat)
        {
            await _chatCollection.InsertOneAsync(chat);
        }

        public async Task Update(Chat chat)
        {
            var filter = Builders<Chat>.Filter.Eq(c => c.Id, chat.Id);
            await _chatCollection.ReplaceOneAsync(filter, chat);
        }
    }
}
