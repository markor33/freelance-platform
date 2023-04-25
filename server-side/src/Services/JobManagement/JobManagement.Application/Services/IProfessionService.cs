using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;

namespace JobManagement.Application.Services
{
    public interface IProfessionService
    {
        Task<Profession> GetByIdAsync(Guid id);
    }
}
