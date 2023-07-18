using EventBus.Events;
using Identity.API.Constants;
using Identity.API.Data;
using Identity.API.IntegrationEvents.Events;
using Identity.API.Models;
using Identity.API.Models.ViewModels;
using IntegrationEventLog.EFCore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Identity.API.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIntegrationEventLogService _integrationEventLogService;

        public RegisterService(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _integrationEventLogService = integrationEventLogServiceFactory(dbContext.Database.GetDbConnection());
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            var newUser = new ApplicationUser()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Username
            };

            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            var result =  await _userManager.CreateAsync(newUser, registerViewModel.Password);
            if (!result.Succeeded)
                return result;

            await _userManager.AddToRoleAsync(newUser, registerViewModel.Role.ToString());

            IntegrationEvent @event;
            if (registerViewModel.Role == Role.FREELANCER)
                @event = new FreelancerRegisteredIntegrationEvent(newUser.Id, registerViewModel.FirstName, registerViewModel.LastName, registerViewModel.Contact);
            else
                @event = new ClientRegisteredIntegrationEvent(newUser.Id, registerViewModel.FirstName, registerViewModel.LastName, registerViewModel.Contact);
            await _integrationEventLogService.SaveEventAsync(@event, transaction);

            await transaction.CommitAsync();
            return result;
        }
    }
}
