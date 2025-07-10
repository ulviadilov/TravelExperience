using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelExperience.Application.Features.Trips.Commands.CreateTrip;
using TravelExperience.Application.Features.Trips.Queries.GetTripById;
using TravelExperience.Application.Features.Trips.Queries.GetTripByUserId;
using TravelExperience.WebAPI.Enums;
using TravelExperience.WebAPI.Extensions;

namespace TravelExperience.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TripsController> _logger;

    public TripsController(IMediator mediator, ILogger<TripsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new trip for a user with specified title, dates, and activities.
    /// </summary>
    /// <remarks>
    /// Sample request: POST /api/trips
    /// 
    /// Request body:
    /// {
    ///     "userId": "user12345",
    ///     "title": "Summer Vacation in Italy",
    ///     "startDate": "2025-08-01T00:00:00",
    ///     "endDate": "2025-08-10T00:00:00",
    ///     "activities": [
    ///         {
    ///             "destinationId": 1,
    ///             "duration": 4,
    ///             "cost": 250.5
    ///         },
    ///         {
    ///             "destinationId": 2,
    ///             "duration": 6,
    ///             "cost": 400.0
    ///         }
    ///     ]
    /// }
    /// </remarks>
    /// <param name="command">The CreateTripCommand object containing trip details.</param>
    /// <returns>
    /// Returns the created trip's information on success (HTTP 200), validation errors (HTTP 400), or internal errors (HTTP 500).
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<CreateTripResponse>> CreateTrip([FromBody] CreateTripCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            _logger.LogInformation("Trip created successfully with ID: {TripId}", response.TripId);
            return Ok(response);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation failed");
            var errors = ex.Errors.Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });
            return BadRequest(new { errors });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error: {Message}", ErrorMessages.TripCreationFailed.GetMessage());
            return StatusCode((int)ResponseStatusCode.InternalServerError, new { error = ErrorMessages.TripCreationFailed.GetMessage() });
        }
    }


    /// <summary>
    /// Retrieves the details of a specific trip by its ID.
    /// </summary>
    /// <remarks>
    /// Sample request: GET /api/trips/1
    /// </remarks>
    /// <param name="tripId">The ID of the trip to retrieve.</param>
    /// <returns>
    /// Returns the trip details (HTTP 200) if found, 
    /// a not found error (HTTP 404) if the trip doesn't exist, 
    /// validation errors (HTTP 400) if the trip ID is invalid,
    /// or internal server errors (HTTP 500) if an unexpected error occurs.
    /// </returns>

    [HttpGet("{tripId}")]
    public async Task<ActionResult<GetTripByIdResponse>> GetTripById(int tripId)
    {
        try
        {
            var query = new GetTripByIdQuery { TripId = tripId };
            var response = await _mediator.Send(query);
            if (response == null)
                return NotFound(new { error = ErrorMessages.TripNotFound.GetMessage() });

            return Ok(response);
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });
            return BadRequest(new { errors });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error: {Message}", ErrorMessages.GetTripByIdFailed.GetMessage());
            return StatusCode((int)ResponseStatusCode.InternalServerError, new { error = ErrorMessages.GetTripByIdFailed.GetMessage() });
        }
    }

    /// <summary>
    /// Retrieves all trips associated with a specific user.
    /// </summary>
    /// <remarks>
    /// Sample request: GET /api/trips/user/user12345
    /// </remarks>
    /// <param name="userId">The ID of the user whose trips are to be retrieved.</param>
    /// <returns>
    /// Returns a list of trips (HTTP 200) for the specified user, 
    /// validation errors (HTTP 400) if the user ID is invalid,
    /// or internal server errors (HTTP 500) if an unexpected error occurs.
    /// </returns>

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<GetTripsByUserIdResponse>> GetTripsByUserId(string userId)
    {
        try
        {
            var query = new GetTripsByUserIdQuery { UserId = userId };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });
            return BadRequest(new { errors });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error: {Message}", ErrorMessages.GetTripsFailed.GetMessage());
            return StatusCode((int)ResponseStatusCode.InternalServerError, new { error = ErrorMessages.GetTripsFailed.GetMessage() });
        }
    }

}