using JobManagement.Domain.AggregatesModel.JobAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobManagement.Infrastructure.EntityConfiguration
{
    public class JobEntityTypeConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Jobs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ClientId).IsRequired();

            builder.Property(x => x.Title).IsRequired();

            builder.Property(x => x.Description).IsRequired();

            builder.Property(x => x.Credits).IsRequired();

            builder.Property(x => x.ExperienceLevel).IsRequired();

            builder.Property(x => x.Status).IsRequired();

            builder.OwnsOne(x => x.Payment);

            builder.HasMany(x => x.Questions).WithOne().IsRequired();

            builder.HasMany(x => x.Proposals).WithOne().IsRequired();

            builder.HasMany(x => x.Skills)
                .WithMany(s => s.Jobs);
        }
    }
}
