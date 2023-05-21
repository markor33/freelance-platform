using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobManagement.Infrastructure.EntityConfiguration
{
    public class ContractEntityTypeConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contracts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ClientId).IsRequired();

            builder.Property(x => x.FreelancerId).IsRequired();

            builder.Property(x => x.Started).IsRequired();

            builder.OwnsOne(x => x.Payment);
        }
    }
}
