using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerProfile.Infrastructure.EntityConfiguration
{
    public class FreelancerEntityTypeConfiguration : IEntityTypeConfiguration<Freelancer>
    {
        public void Configure(EntityTypeBuilder<Freelancer> freelancerConfiguration)
        {
            freelancerConfiguration.ToTable("Freelancers");

            freelancerConfiguration.HasKey(f => f.Id);

            freelancerConfiguration.Property(f => f.UserId).IsRequired();
            freelancerConfiguration.HasIndex(f => f.UserId).IsUnique();

            freelancerConfiguration.Property(f => f.FirstName).IsRequired().HasMaxLength(100);

            freelancerConfiguration.Property(f => f.LastName).IsRequired().HasMaxLength(100);

            freelancerConfiguration.OwnsOne(f => f.Contact, c =>
            {
                c.Ignore(c => c.TimeZone);

                c.OwnsOne(c => c.Address);
            });
        }
    }
}
