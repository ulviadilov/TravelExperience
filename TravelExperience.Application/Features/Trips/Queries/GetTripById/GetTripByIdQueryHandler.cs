using MediatR;
using TravelExperience.Application.DTOs;
using TravelExperience.Application.Interfaces;

namespace TravelExperience.Application.Features.Trips.Queries.GetTripById
{
    public class GetTripByIdQueryHandler : IRequestHandler<GetTripByIdQuery, GetTripByIdResponse?>
    {
        private readonly ITripService _tripService;

        public GetTripByIdQueryHandler(ITripService tripService)
        {
            _tripService = tripService;
        }

        public async Task<GetTripByIdResponse?> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
        {
            var trip = await _tripService.GetByIdAsync(request.TripId);

            if (trip == null)
                return null;

            return new GetTripByIdResponse
            {
                TripId = trip.TripId,
                UserId = trip.UserId,
                Title = trip.Title,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                TotalCost = trip.TotalCost,
                CreatedAt = trip.CreatedAt,
                UpdatedAt = trip.UpdatedAt,
                Activities = trip.Activities.Select(a => new ActivityDto
                {
                    ActivityId = a.ActivityId,
                    DestinationId = a.DestinationId,
                    Duration = a.Duration,
                    Cost = a.Cost,
                    CreatedAt = a.CreatedAt
                }).ToList()
            };
        }
    }
}