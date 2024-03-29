using EventBus;
using EventBus.Abstractions;
using EventBus.Extensions;
using EventBusRabbitMQ;
using IntegrationEventLog.EFCore.Services;
using IntegrationEventLog.EFCore;
using JobManagement.API.GrpcServices;
using JobManagement.API.Security;
using JobManagement.Application;
using JobManagement.Application.IntegrationEvents.Events;
using JobManagement.Application.IntegrationEvents.Handlers;
using JobManagement.Application.Notifications;
using JobManagement.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using RabbitMQ.Client;
using System.Data;
using System.Data.Common;
using System.Net;
using JobManagement.Infrastructure.LoadingStrategy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("JobManagemenet");
builder.Services.AddDbContext<JobManagementContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IDbConnection>(provider => new NpgsqlConnection(connectionString));

var loadingStrategySettings = builder.Configuration.GetSection("LoadingStrategy");
builder.Services.Configure<LoadingStrategySettings>(loadingStrategySettings);

builder.Services.AddDbContext<IntegrationEventLogContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(sp => (DbConnection c) => new IntegrationEventLogService(c));
builder.Services.AddIntegrationEventsList(typeof(ProposalCreatedIntegrationEvent).Assembly);

builder.Services.AddHostedService<IntegrationEventSenderService>();

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
    var subscriptionClientName = "job-management";
    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

    return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, eventBusSubcriptionsManager, sp, 5, subscriptionClientName);
});

builder.Services.AddIntegrationEventsHandlers(typeof(CreditsReservedIntegrationEventHandler).Assembly);

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
eventBus.AddHandlers(typeof(CreditsReservedIntegrationEventHandler).Assembly);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<ProposalGrpcService>();
app.MapGrpcService<JobGrpcService>();
app.MapGrpcService<ContractGrpcService>();

app.Run();

public partial class Program { }
