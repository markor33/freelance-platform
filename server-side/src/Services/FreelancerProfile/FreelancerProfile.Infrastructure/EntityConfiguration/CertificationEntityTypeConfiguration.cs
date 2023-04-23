using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FreelancerProfile.Infrastructure.EntityConfiguration
{
    public class CertificationEntityTypeConfiguration : IEntityTypeConfiguration<Certification>
    {
        public void Configure(EntityTypeBuilder<Certification> builder)
        {
            builder.ToTable("Certifications");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired();

            builder.Property(c => c.Provider).IsRequired();

            builder.OwnsOne(c => c.Attended, a =>
            {
                a.Property(a => a.Start).HasColumnType("date");
                a.Property(a => a.End).HasColumnType("date");
            });

            builder.Property(c => c.Description).IsRequired(false);
        }
    }
}
