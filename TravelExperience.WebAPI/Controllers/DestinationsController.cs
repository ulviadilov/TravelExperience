using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelExperience.Application.Features.Destinations.Queries.GetAllDestinations;

namespace TravelExperience.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DestinationsController> _logger;

        public DestinationsController(IMediator mediator, ILogger<DestinationsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<GetAllDestinationsResponse>> GetAllDestinations()
        {
            try
            {
                var query = new GetAllDestinationsQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving destinations");
                return StatusCode(500, new { error = "An error occurred while retrieving destinations" });
            }
        }
    }
}
