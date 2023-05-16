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
    });
builder.Services.AddAuthorization();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddScoped(typeof(IMongoDbFactory), typeof(MongoDbFactory));

builder.Services.AddScoped(typeof(INotificationRepository), typeof(NotificationRepository));

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

builder.Services.AddScoped<ProposalSubmittedNotificationHandler>();

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<ProposalSubmittedNotification, ProposalSubmittedNotificationHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<NotificationHub>("/hub/notifications");

app.MapControllers();

app.Run();
