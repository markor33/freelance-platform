using Microsoft.AspNetCore.Mvc;
using Web.Bff.Models;
using System.IdentityModel.Tokens.Jwt;
using Web.Bff.Extensions;
using Web.Bff.Services;
using FluentResults;
using GrpcFreelancerProfile;
using Grpc.Core;
using GrpcClientProfile;

namespace Web.Bff.Controllers
{
    [Route("api/aggregator/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IClientProfileService _clientProfileService;
        private readonly IFreelancerProfileService _freelancerProfileService;
        private readonly IIdentityService _identityService;

        public UserController(
            IClientProfileService clientProfileService,
            IFreelancerProfileService freelancerProfileService, 
            IIdentityService identityService)
        {
            _clientProfileService = clientProfileService;
            _freelancerProfileService = freelancerProfileService;
            _identityService = identityService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var loginResult = await _identityService.LoginAsync(request.Username, request.Password);
            if (loginResult.IsFailed)
                return BadRequest();

            var accessToken = loginResult.Value;

            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.ReadJwtToken(accessToken);
            var userId = token.Claims.GetUserId();
            var role = token.Claims.GetRole();

            Result<LoginResponse> result = null;
            if (role == "FREELANCER")
                result = await HandleFreelancerLogin(userId);
            else if (role == "CLIENT")
                result = await HandleClientLogin(userId);
            result.Value.Jwt = accessToken;

            return Ok(result.Value);
        }

        private async Task<Result<LoginResponse>> HandleFreelancerLogin(string userId)
        {
            FreelancerBasicData freelancerBasicData;
            try
            {
                freelancerBasicData = await _freelancerProfileService.GetBasicDataByUserIdAsync(userId);
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                return Result.Ok(new LoginResponse());
            }

            var response = new LoginResponse()
            {
                DomainUserId = Guid.Parse(freelancerBasicData.Id),
                FirstName = freelancerBasicData.FirstName,
                LastName = freelancerBasicData.LastName,
                ProfessionId = Guid.Parse(freelancerBasicData.ProfessionId)
            };
            return Result.Ok(response);
        }

        private async Task<Result<LoginResponse>> HandleClientLogin(string userId)
        {
            ClientBasicData clientBasicData;
            try
            {
                clientBasicData = await _clientProfileService.GetBasicDataByUserIdAsync(userId);
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                return Result.Ok(new LoginResponse());
            }

            var response = new LoginResponse()
            {
                DomainUserId = Guid.Parse(clientBasicData.Id),
                FirstName = clientBasicData.FirstName,
                LastName = clientBasicData.LastName
            };
            return Result.Ok(response);
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("asdasdasd");
        }

    }
}
