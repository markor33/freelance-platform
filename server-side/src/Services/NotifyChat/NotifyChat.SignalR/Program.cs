using EventBus.Abstractions;
using EventBus;
using EventBusRabbitMQ;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using NotifyChat.SignalR.Hubs;
using NotifyChat.Notifications.IntegrationEvents;
using NotifyChat.SignalR.Notifications.Handlers;
using NotifyChat.SignalR.Persistence.Settings;
using NotifyChat.SignalR.Persistence.Repositories;
using NotifyChat.SignalR.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using NotifyChat.SignalR.GrpcServices;
using NotifyChat.SignalR.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

        options.Events = new JwtBearerEvents()
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/hub")))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddScoped(typeof(IMongoDbFactory), typeof(MongoDbFactory));

builder.Services.AddScoped(typeof(INotificationRepository), typeof(NotificationRepository));
builder.Services.AddScoped(typeof(IChatRepository), typeof(ChatRepository));
builder.Services.AddScoped(typeof(IMessageRepository), typeof(MessageRepository));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(typeof(IIdentityService), typeof(IdentityService));

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
    var subscriptionClientName = "notify-chat";
    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

    return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, eventBusSubcriptionsManager, sp, 5, subscriptionClientName);
});

builder.Services.AddSignalR();
builder.Services.AddSingleton(typeof(IActiveUsersService), typeof(ActiveUsersService));

builder.Services.AddScoped<ProposalSubmittedNotificationHandler>();
builder.Services.AddScoped<InterviewStageStartedNotificationHandler>();
builder.Services.AddScoped<ProposalPaymentChangedNotificationHandler>();

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
eventBus.Subscribe<ProposalSubmittedNotification, ProposalSubmittedNotificationHandler>();
eventBus.Subscribe<InterviewStageStartedNotification, InterviewStageStartedNotificationHandler>();
eventBus.Subscribe<ProposalPaymentChangedNotification, ProposalPaymentChangedNotificationHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<ChatGrpcService>();

app.MapHub<NotificationHub>("/hub/notifications");
app.MapHub<ChatHub>("hub/chat");

app.MapControllers();

app.Run();

public partial class Program { }
