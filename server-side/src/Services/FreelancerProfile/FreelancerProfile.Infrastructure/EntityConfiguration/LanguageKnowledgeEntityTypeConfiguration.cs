using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerProfile.Infrastructure.EntityConfiguration
{
    public class LanguageKnowledgeEntityTypeConfiguration : IEntityTypeConfiguration<LanguageKnowledge>
    {
        public void Configure(EntityTypeBuilder<LanguageKnowledge> builder)
        {
            builder.ToTable("LanguageKnowledges");

            builder.HasKey(lk => lk.Id);

            builder.HasOne(lk => lk.Language);

            builder.Property(lk => lk.ProfiencyLevel).IsRequired();
        }
    }
}
