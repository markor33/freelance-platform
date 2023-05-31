using Microsoft.EntityFrameworkCore;
using ProfileManagemenet.Infrastructure;
using FreelancerProfile.Application;
using Microsoft.IdentityModel.Tokens;
using FreelancerProfile.API.Security;
using EventBus.Abstractions;
using EventBus;
using EventBusRabbitMQ;
using RabbitMQ.Client;
using FreelancerProfile.Application.IntegrationEvents.Handlers;
using Npgsql;
using System.Data;
using FreelancerProfile.API.GrpcServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using EventBus.Extensions;
using FreelancerProfile.Infrastructure.Persistence.ReadModel.Settings;
using FreelancerProfile.Infrastructure.Persistence;
using FreelancerProfile.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("FreelancerProfile");
builder.Services.AddDbContext<FreelancerProfileContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IDbConnection>(provider => new NpgsqlConnection(connectionString));

var mongoDbSettings = builder.Configuration.GetSection("MongoDB");
builder.Services.Configure<MongoDBSettings>(mongoDbSettings);

var azureBlobStorageSettings = builder.Configuration.GetSection("Azure");
builder.Services.Configure<AzureBlobStorageSettings>(azureBlobStorageSettings);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://host.docker.internal:50000";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient(typeof(IIdentityService), typeof(IdentityService));

builder.Services.AddApplication();
builder.Services.AddInfrastructure();


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
    var subscriptionClientName = "freelancer-profile";
    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

    return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, eventBusSubcriptionsManager, sp, 5, subscriptionClientName);
});

builder.Services.AddIntegrationEventsHandlers(typeof(ProposalCreatedIntegrationEventHandler).Assembly);

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
});

builder.WebHost.UseKestrel(options => {
    options.Listen(IPAddress.Any, 80, listenOptions => {
        listenOptions.Protocols = HttpProtocols.Http1;
    });

    options.Listen(IPAddress.Any, 5000, listenOptions => {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.AddHandlers(typeof(ProposalCreatedIntegrationEventHandler).Assembly);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<FreelancerProfileGrpcService>();

app.Run();

public partial class Program { }
