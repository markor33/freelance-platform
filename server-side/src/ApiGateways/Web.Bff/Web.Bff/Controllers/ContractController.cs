using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Bff.Models;
using Web.Bff.Services;

namespace Web.Bff.Controllers
{
    [Route("api/aggregator/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;
        private readonly IIdentityService _identityService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet("client/{id}")]
        [Authorize(Roles = "CLIENT")]
        public async Task<ActionResult<List<Contract>>> GetByClient(Guid id)
        {
            return await _contractService.GetByClient(id);
        }

        [HttpGet("job/{id}")]
        [Authorize(Roles = "CLIENT")]
        public async Task<ActionResult<List<Contract>>> GetByJob(Guid id)
        {
            return await _contractService.GetByJob(id);
        }

    }
}
