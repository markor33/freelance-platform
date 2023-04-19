using JobManagement.Domain.AggregatesModel.JobAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobManagement.Infrastructure.EntityConfiguration
{
    public class JobEntityConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Jobs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ClientUserId).IsRequired();

            builder.Property(x => x.Title).IsRequired();

            builder.Property(x => x.Description).IsRequired();
        }
    }
}
