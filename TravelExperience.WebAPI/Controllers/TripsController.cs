using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelExperience.Application.Features.Trips.Commands.CreateTrip;
using TravelExperience.Application.Features.Trips.Queries.GetTripById;
using TravelExperience.Application.Features.Trips.Queries.GetTripByUserId;

namespace TravelExperience.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TripsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateTripResponse>> CreateTrip([FromBody] CreateTripCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while creating the trip" });
        }
    }

    [HttpGet("{tripId}")]
    public async Task<ActionResult<GetTripByIdResponse>> GetTripById(int tripId)
    {
        try
        {
            var query = new GetTripByIdQuery { TripId = tripId };
            var response = await _mediator.Send(query);

            if (response == null)
                return NotFound(new { error = "Trip not found" });

            return Ok(response);
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });
            return BadRequest(new { errors });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving the trip" });
        }
    }

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
            return StatusCode(500, new { error = "An error occurred while retrieving trips" });
        }
    }
}