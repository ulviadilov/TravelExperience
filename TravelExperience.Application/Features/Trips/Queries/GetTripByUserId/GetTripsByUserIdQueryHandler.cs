using MediatR;
using TravelExperience.Application.DTOs;
using TravelExperience.Application.Features.Trips.Queries.GetTripById;
using TravelExperience.Application.Interfaces;

namespace TravelExperience.Application.Features.Trips.Queries.GetTripByUserId
{
    public class GetTripsByUserIdQueryHandler : IRequestHandler<GetTripsByUserIdQuery, GetTripsByUserIdResponse>
    {
        private readonly ITripService _tripService;

        public GetTripsByUserIdQueryHandler(ITripService tripService)
        {
            _tripService = tripService;
        }

        /// <summary>
        /// Handles the query to retrieve all trips associated with a specific user ID.
        /// </summary>
        /// <param name="request">The query containing the user ID.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A response containing a list of trip details for the specified user.</returns>
        /// 
        public async Task<GetTripsByUserIdResponse> Handle(GetTripsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var trips = await _tripService.GetByUserIdAsync(request.UserId);

            return new GetTripsByUserIdResponse
            {
                Trips = trips.Select(trip => new GetTripByIdResponse
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
                }).ToList()
            };
        }
    }
}