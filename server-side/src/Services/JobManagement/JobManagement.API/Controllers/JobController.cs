using AutoMapper;
using JobManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IAnswerQueries _answerQueries;

        public JobController(
            IMediator mediator,
            IMapper mapper,
            IJobQueries jobQueries,
            IProposalQueries proposalQueries,
            IAnswerQueries answerQueries)
        {
            _mediator = mediator;
            _mapper = mapper;
            _jobQueries = jobQueries;
            _proposalQueries = proposalQueries;
            _answerQueries = answerQueries;
        }

    }
}
