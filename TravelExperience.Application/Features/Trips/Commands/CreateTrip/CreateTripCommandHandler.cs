using MediatR;
using TravelExperience.Application.DTOs;
using TravelExperience.Application.Interfaces;
using TravelExperience.Domain.Models;

namespace TravelExperience.Application.Features.Trips.Commands.CreateTrip;
public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, CreateTripResponse>
{
    private readonly ITripService _tripService;

    public CreateTripCommandHandler(ITripService tripService)
    {
        _tripService = tripService;
    }

    /// <summary>
    /// Handles the command to create a new trip with associated activities.
    /// </summary>
    /// <param name="request">The command containing trip and activity details.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A response containing the details of the created trip.</returns>
    public async Task<CreateTripResponse> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        var trip = new Trip
        {
            UserId = request.UserId,
            Title = request.Title,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        foreach (var activityDto in request.Activities)
        {
            var activity = new Activity
            {
                DestinationId = activityDto.DestinationId,
                Duration = activityDto.Duration,
                Cost = activityDto.Cost,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            trip.Activities.Add(activity);
        }

        trip.TotalCost = trip.Activities.Sum(a => a.Cost);

        var createdTrip = await _tripService.CreateAsync(trip);

        return new CreateTripResponse
        {
            TripId = createdTrip.Id,
            UserId = createdTrip.UserId,
            Title = createdTrip.Title,
            StartDate = createdTrip.StartDate,
            EndDate = createdTrip.EndDate,
            TotalCost = createdTrip.TotalCost,
            CreatedAt = createdTrip.CreatedAt,
            Activities = createdTrip.Activities.Select(a => new ActivityDto
            {
                ActivityId = a.Id,
                DestinationId = a.DestinationId,
                Duration = a.Duration,
                Cost = a.Cost,
                CreatedAt = a.CreatedAt
            }).ToList()
        };
    }
}