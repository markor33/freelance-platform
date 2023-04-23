using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobManagement.Infrastructure.EntityConfiguration
{
    public class ProposalEntityTypeConfiguration : IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {
            builder.ToTable("Proposals");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FreelancerId).IsRequired();

            builder.Property(x => x.Text).IsRequired();

            builder.Property(x => x.ProposalStatus).IsRequired(false);

            builder.OwnsOne(x => x.Payment);

            builder.HasMany(x => x.Answers).WithOne().IsRequired();
        }
    }
}
