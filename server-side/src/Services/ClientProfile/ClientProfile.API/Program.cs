using ClientProfile.API.Grpc;
using ClientProfile.API.Infrastructure;
using ClientProfile.API.Infrastructure.Repositories;
using ClientProfile.API.Security;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClientProfileContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ClientProfile")));

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

builder.Services.AddTransient(typeof(IClientRepository), typeof(ClientRepository));

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<ClientProfileGrpcService>();

app.Run();
