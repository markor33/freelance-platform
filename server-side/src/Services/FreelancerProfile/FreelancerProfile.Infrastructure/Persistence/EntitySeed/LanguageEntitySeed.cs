﻿using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerProfile.Infrastructure.Persistence.EntitySeed
{
    public static class LanguageEntitySeed
    {
        public static EntityTypeBuilder<Language> SeedLanguage(this EntityTypeBuilder<Language> builder)
        {
            builder.HasData(new Language[]
            {
                new Language(1, "English", "en"),
                new Language(2, "Serbian", "sr"),
                new Language(3, "German", "de")
            });

            return builder;
        }
    }
}
