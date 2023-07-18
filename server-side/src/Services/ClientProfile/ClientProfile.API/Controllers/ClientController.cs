using ClientProfile.API.Infrastructure.Repositories;
using ClientProfile.API.Model;
using ClientProfile.API.Security;
using Microsoft.AspNetCore.Mvc;

namespace ClientProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IIdentityService _identityService;
    
        public ClientController(IClientRepository clientRepository, IIdentityService identityService)
        {
            _clientRepository = clientRepository;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<ActionResult<Client>> Get()
        {
            var userId = _identityService.GetUserId();
            var client = await _clientRepository.GetByUserIdAsync(userId);
            if (client is null)
                return BadRequest();
            return Ok(client);
        }

    }
}
