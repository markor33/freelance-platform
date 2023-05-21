using GrpcClientProfile;
using GrpcFreelancerProfile;
using GrpcJobManagement;
using GrpcNotifyChat;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Web.Bff.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddOcelot();

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

builder.Services.AddGrpcClient<FreelancerProfile.FreelancerProfileClient>((services, options) =>
{
    options.Address = new Uri("http://host.docker.internal:52001");
});
builder.Services.AddGrpcClient<ClientProfile.ClientProfileClient>((services, options) =>
{
    options.Address = new Uri("http://host.docker.internal:53001");
});
builder.Services.AddGrpcClient<Job.JobClient>((services, options) =>
{
    options.Address = new Uri("http://host.docker.internal:51001");
});
builder.Services.AddGrpcClient<Proposal.ProposalClient>((services, options) =>
{
    options.Address = new Uri("http://host.docker.internal:51001");
});
builder.Services.AddGrpcClient<Contract.ContractClient>((services, options) =>
{
    options.Address = new Uri("http://host.docker.internal:51001");
});
builder.Services.AddGrpcClient<ChatService.ChatServiceClient>((services, options) =>
{
    options.Address = new Uri("http://host.docker.internal:62001");
});

builder.Services.AddTransient(typeof(IClientProfileService), typeof(ClientProfileService));
builder.Services.AddTransient(typeof(IFreelancerProfileService), typeof(FreelancerProfileService));
builder.Services.AddTransient(typeof(IJobManagementService), typeof(JobManagementService));
builder.Services.AddTransient(typeof(IProposalService), typeof(ProposalService));
builder.Services.AddTransient(typeof(IContractService), typeof(ContractService));
builder.Services.AddTransient(typeof(IIdentityService), typeof(IdentityService));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}");
});

app.MapControllers();

app.UseWebSockets();
await app.UseOcelot();

app.Run();
