using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobManagement.Infrastructure.EntityConfiguration
{
    public class ProfessionEntityTypeConfiguration : IEntityTypeConfiguration<Profession>
    {
        public void Configure(EntityTypeBuilder<Profession> builder)
        {
            builder.ToTable("Professions");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

            builder.Property(p => p.Description).IsRequired().HasMaxLength(100);

            builder.HasMany(p => p.Skills);
        }
    }
}
