using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerProfile.Infrastructure.EntityConfiguration
{
    public class EducationEntityTypeConfiguration : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.ToTable("Educations");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.SchoolName).IsRequired();

            builder.Property(e => e.Degree).IsRequired();

            builder.OwnsOne(e => e.Attended, a =>
            {
                a.Property(a => a.Start).HasColumnType("date");
                a.Property(a => a.End).HasColumnType("date");
            });
        }
    }
}
