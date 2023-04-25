using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobManagement.Infrastructure.EntityConfiguration
{
    public class SkillEntityTypeConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable("Skills");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(50);

            builder.Property(s => s.Description).IsRequired().HasMaxLength(100);

            builder.HasOne(s => s.Profession);

            builder.Ignore("JobId");
        }
    }
}
