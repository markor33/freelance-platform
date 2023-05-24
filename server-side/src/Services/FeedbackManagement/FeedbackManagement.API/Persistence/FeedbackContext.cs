using FeedbackManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedbackManagement.API.Persistence
{
    public class FeedbackContext : DbContext
    {
        public DbSet<FinishedContract> FinishedContracts { get; set; }

        public FeedbackContext(DbContextOptions<FeedbackContext> options) : base(options)
        {

        }
    }
}
