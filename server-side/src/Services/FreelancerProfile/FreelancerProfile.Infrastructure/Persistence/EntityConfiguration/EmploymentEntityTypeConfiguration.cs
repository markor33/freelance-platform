using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerProfile.Infrastructure.Persistence.EntityConfiguration
{
    public class EmploymentEntityTypeConfiguration : IEntityTypeConfiguration<Employment>
    {
        public void Configure(EntityTypeBuilder<Employment> builder)
        {
            builder.ToTable("Employments");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Company).IsRequired();

            builder.Property(e => e.Title).IsRequired();

            builder.OwnsOne(e => e.Period, p =>
            {
                p.Property(p => p.Start).HasColumnType("date");
                p.Property(p => p.End).HasColumnType("date");
            });

            builder.Property(e => e.Description).IsRequired(false);
        }
    }
}
