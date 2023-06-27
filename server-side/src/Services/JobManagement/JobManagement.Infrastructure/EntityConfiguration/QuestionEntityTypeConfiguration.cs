using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobManagement.Infrastructure.EntityConfiguration
{
    public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Text).IsRequired();
        }
    }
}
