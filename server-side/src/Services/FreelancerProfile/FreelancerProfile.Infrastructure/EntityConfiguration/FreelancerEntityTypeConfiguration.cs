using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerProfile.Infrastructure.EntityConfiguration
{
    public class FreelancerEntityTypeConfiguration : IEntityTypeConfiguration<Freelancer>
    {
        public void Configure(EntityTypeBuilder<Freelancer> builder)
        {
            builder.ToTable("Freelancers");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.UserId).IsRequired();
            builder.HasIndex(f => f.UserId).IsUnique();

            builder.Property(f => f.FirstName).IsRequired().HasMaxLength(100);

            builder.Property(f => f.LastName).IsRequired().HasMaxLength(100);

            builder.OwnsOne(f => f.Contact, c =>
            {
                c.Ignore(c => c.TimeZone);

                c.OwnsOne(c => c.Address);
            });

            builder.OwnsOne(f => f.ProfileSummary);

            builder.OwnsOne(f => f.HourlyRate);

            builder.Property(f => f.Joined).HasColumnType("date");

            builder.HasMany(f => f.Skills)
                .WithMany(s => s.Freelancers);

            builder.HasMany(f => f.LanguageKnowledges).WithOne().IsRequired();

            builder.HasMany(f => f.Educations).WithOne().IsRequired();

            builder.HasMany(f => f.Certifications).WithOne().IsRequired();

            builder.HasMany(f => f.Employments).WithOne().IsRequired();

            builder.HasMany(f => f.PortfolioProjects).WithOne().IsRequired();
        }
    }
}
