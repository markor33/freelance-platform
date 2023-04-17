using ClientProfile.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientProfile.API.Infrastructure.EntityConfiguration
{
    public class ClientEntityConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired();

            builder.Property(x => x.LastName).IsRequired();

            builder.OwnsOne(x => x.Contact, c =>
            {
                c.Ignore(c => c.TimeZone);

                c.OwnsOne(c => c.Address);
            });
        }
    }
}
