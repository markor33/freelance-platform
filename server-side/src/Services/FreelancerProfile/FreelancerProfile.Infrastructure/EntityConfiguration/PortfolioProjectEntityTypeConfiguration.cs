using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerProfile.Infrastructure.EntityConfiguration
{
    public class PortfolioProjectEntityTypeConfiguration : IEntityTypeConfiguration<PortfolioProject>
    {
        public void Configure(EntityTypeBuilder<PortfolioProject> builder)
        {
            builder.ToTable("PortfolioProjects");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title).IsRequired();

            builder.Property(p => p.Description).IsRequired();

            builder.OwnsOne(p => p.Period, p =>
            {
                p.Property(p => p.Start).HasColumnType("date");
                p.Property(p => p.End).HasColumnType("date");
            });
        }
    }
}
