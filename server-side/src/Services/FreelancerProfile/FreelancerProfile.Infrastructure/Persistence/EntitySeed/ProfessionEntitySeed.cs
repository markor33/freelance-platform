using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.Persistence.EntitySeed
{
    public static class ProfessionEntitySeed
    {
        public static ModelBuilder SeedProfession(this ModelBuilder builder)
        {
            var p1Id = Guid.Parse("d6861f65-0950-4c7f-b5b1-de644f923fbb");
            var p2Id = Guid.Parse("0c485898-d9f4-45c5-99bc-c2c8dd3e69f0");
            builder.Entity<Profession>().HasData(
                new
                {
                    Id = p1Id,
                    Name = "Software engineer",
                    Description = "Software engineer"
                },
                new
                {
                    Id = p2Id,
                    Name = "Graphic designer",
                    Description = "Graphic designer"
                });

            builder.Entity<Skill>().HasData(
                new
                {
                    Id = Guid.Parse("93098c08-85ff-4c31-994b-5dec79c17d79"),
                    Name = "C#",
                    Description = "Programming language",
                    ProfessionId = p1Id
                },
                new
                {
                    Id = Guid.Parse("ea1627e1-2d59-427d-b5b4-13ab7e944c7f"),
                    Name = "ASP.NET CORE",
                    Description = "Web framework",
                    ProfessionId = p1Id
                },
                new
                {
                    Id = Guid.Parse("5d741f6a-f024-4dca-8b1f-afccec1f72ea"),
                    Name = "Adobe Illustrator",
                    Description = "Design software",
                    ProfessionId = p2Id
                },
                new
                {
                    Id = Guid.Parse("e190ca8a-5252-4b00-8128-f21d9918efaf"),
                    Name = "CorelDRAW Graphics Suite",
                    Description = "Design software",
                    ProfessionId = p2Id
                });

            return builder;
        }
    }

}
