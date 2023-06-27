using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobManagement.Infrastructure.EntityConfiguration
{
    public class AnswerEntityTypeConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.QuestionId).IsRequired();

            builder.Property(x => x.Text).IsRequired();
        }
    }
}
