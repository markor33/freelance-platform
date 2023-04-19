using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.EntitySeed
{
    public static class ProfessionEntitySeed
    {
        public static ModelBuilder SeedProfession(this ModelBuilder builder)
        {
            var p1Id = Guid.NewGuid();
            var p2Id = Guid.NewGuid();
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
                    Id = Guid.NewGuid(),
                    Name = "Java",
                    Description = "Programming language",
                    ProfessionId = p1Id
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Name = "C#",
                    Description = "Programming language",
                    ProfessionId = p1Id
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Name = "ASP>NET CORE",
                    Description = "Web framework",
                    ProfessionId = p1Id
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Name = "Adobe Illustrator",
                    Description = "Design software",
                    ProfessionId = p2Id
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Name = "Adobe Photoshop",
                    Description = "Design software",
                    ProfessionId = p2Id
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Name = "CorelDRAW Graphics Suite",
                    Description = "Design software",
                    ProfessionId = p2Id
                });

            return builder;
        }
    }

}
