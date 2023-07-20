using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerProfile.Infrastructure.Persistence.EventStore
{
    public class DomainEventLogEntityTypeConfiguration : IEntityTypeConfiguration<DomainEventLog>
    {
        public void Configure(EntityTypeBuilder<DomainEventLog> builder)
        {
            builder.ToTable("DomainEventLogs");

            builder.HasKey(x => x.Id);
        }
    }
}
