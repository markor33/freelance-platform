﻿namespace FreelancerProfile.Infrastructure.Persistence.ReadModel.Settings
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }
}
