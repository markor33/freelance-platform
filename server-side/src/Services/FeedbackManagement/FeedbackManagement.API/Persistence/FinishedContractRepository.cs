using FeedbackManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedbackManagement.API.Persistence
{
    public class FinishedContractRepository : IFinishedContractRepository
    {
        private readonly FeedbackContext _context;

        public FinishedContractRepository(FeedbackContext context)
        {
            _context = context;
        }

        public async Task<FinishedContract> GetById(Guid id)
        {
            return await _context.FinishedContracts.Where(fc => fc.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<FinishedContract>> GetByFreelancer(Guid freelancerId)
        {
            return await _context.FinishedContracts
                .Where(fc => fc.FreelancerId == freelancerId && fc.ClientFeedback != null)
                .ToListAsync();
        }

        public async Task Create(FinishedContract finishedContract)
        {
            await _context.FinishedContracts.AddAsync(finishedContract);
            await _context.SaveChangesAsync();
        }

        public async Task<float> GetFreelancerAverageRating(Guid freelancerId)
        {
            try
            {
                var average = await _context.FinishedContracts
                .Where(fc => fc.FreelancerId == freelancerId)
                .AverageAsync(fc => fc.ClientFeedback.Rating);

                return (float)average;
            }
            catch (InvalidOperationException ex)
            {
                return 0;
            }
        }

        public async Task<float> GetClientAverageRating(Guid clientId)
        {
            try 
            {
                var average = await _context.FinishedContracts
                    .Where(fc => fc.ClientId == clientId)
                    .AverageAsync(fc => fc.ClientFeedback.Rating);

                return (float)average;
            }
            catch (InvalidOperationException ex)
            {
                return 0;
            }
}

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
