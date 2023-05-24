using Microsoft.AspNetCore.Mvc;
using Web.Bff.Models;
using Web.Bff.Services;

namespace Web.Bff.Controllers
{
    [Route("api/aggregator/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet("freelancer/{freelancerId}")]
        public async Task<ActionResult<List<Feedback>>> GetByFreelancer(Guid freelancerId)
        {
            return await _feedbackService.GetByFreelancer(freelancerId.ToString());
        }

    }
}
