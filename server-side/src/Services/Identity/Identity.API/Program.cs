using EventBus.Abstractions;
using EventBus;
using EventBus.Extensions;
using EventBusRabbitMQ;
using GrpcClientProfile;
using GrpcFreelancerProfile;
using Identity.API.Configuration;
using Identity.API.Data;
using Identity.API.IntegrationEvents.Events;
using Identity.API.Models;
using Identity.API.Services;
using IntegrationEventLog.EFCore;
using IntegrationEventLog.EFCore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Data.Common;
using Identity.API.GrpcServices;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Identity");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddDbContext<IntegrationEventLogContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(sp => (DbConnection c) => new IntegrationEventLogService(c));
builder.Services.AddIntegrationEventsList(typeof(FreelancerRegisteredIntegrationEvent).Assembly);

builder.Services.AddHostedService<IntegrationEventSenderService>();

builder.Services.AddScoped(typeof(IRegisterService), typeof(RegisterService));
builder.Services.AddScoped(typeof(IUserBasicDomainDataService), typeof(UserBasicDomainDataService));

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryApiResources(Config.GetApis())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddInMemoryClients(Config.GetClients())
    .AddAspNetIdentity<ApplicationUser>()
    .AddProfileService<ProfileService>();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed((host) => true)
    .WithOrigins("http://localhost:4200/");

}));

builder.Services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
{
    var factory = new ConnectionFactory()
    {
        HostName = "host.docker.internal",
        Port = 60000,
        UserName = "guest",
        Password = "guest",
    };

    return new DefaultRabbitMQPersistentConnection(factory, 5);
});
builder.Services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
{
    var subscriptionClientName = "identity";
    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

    return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, eventBusSubcriptionsManager, sp, 5, subscriptionClientName);
});

builder.Services.AddGrpcClient<FreelancerProfile.FreelancerProfileClient>((services, options) =>
{
    options.Address = new Uri("http://host.docker.internal:52001");
});
builder.Services.AddGrpcClient<ClientProfile.ClientProfileClient>((services, options) =>
{
    options.Address = new Uri("http://host.docker.internal:53001");
});

var app = builder.Build();

// await ApplicationDbContextSeed.Seed(app.Services.CreateScope().ServiceProvider);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
