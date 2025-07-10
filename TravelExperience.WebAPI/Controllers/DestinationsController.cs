using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelExperience.Application.Features.Destinations.Queries.GetAllDestinations;
using TravelExperience.WebAPI.Enums;
using TravelExperience.WebAPI.Extensions;

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

        /// <summary>
        /// Retrieves a list of all available destinations.
        /// </summary>
        /// <remarks>
        /// Sample request: GET /api/destinations
        /// </remarks>
        /// <returns>
        /// Returns a list of destinations (HTTP 200) on success, 
        /// or an internal server error (HTTP 500) if something goes wrong.
        /// </returns>

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
                _logger.LogError(ex, "Error: {Message}", ErrorMessages.GetDestinationsFailed.GetMessage());
                return StatusCode((int)ResponseStatusCode.InternalServerError, new { error = ErrorMessages.GetDestinationsFailed.GetMessage() });
            }
        }
    }
}
