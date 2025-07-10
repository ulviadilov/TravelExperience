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

        /// <summary>
        /// Handles the query to retrieve details of a specific trip by its ID.
        /// </summary>
        /// <param name="request">The query containing the trip ID.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A response containing the trip details, or null if the trip is not found.</returns>
        /// 
        public async Task<GetTripByIdResponse?> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
        {
            var trip = await _tripService.GetByIdAsync(request.TripId);

            if (trip == null)
                return null;

            return new GetTripByIdResponse
            {
                TripId = trip.Id,
                UserId = trip.UserId,
                Title = trip.Title,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                TotalCost = trip.TotalCost,
                CreatedAt = trip.CreatedAt,
                UpdatedAt = trip.UpdatedAt,
                Activities = trip.Activities.Select(a => new ActivityDto
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

}