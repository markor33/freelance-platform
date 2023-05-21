using AutoMapper;
using JobManagement.API.Security;
using JobManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace JobManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public partial class JobController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IJobQueries _jobQueries;
        private readonly IProposalQueries _proposalQueries;
        private readonly IContractQueries _contractQueries;
        private readonly IAnswerQueries _answerQueries;
        private readonly IIdentityService _identityService;

        public JobController(
            IMediator mediator,
            IMapper mapper,
            IJobQueries jobQueries,
            IProposalQueries proposalQueries,
            IContractQueries contractQueries,
            IAnswerQueries answerQueries,
            IIdentityService identityService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _jobQueries = jobQueries;
            _proposalQueries = proposalQueries;
            _contractQueries = contractQueries;
            _answerQueries = answerQueries;
            _identityService = identityService;
        }

    }
}
