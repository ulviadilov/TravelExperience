using MediatR;

namespace TravelExperience.Application.Features.Trips.Queries.GetTripById
{
    public class GetTripByIdQuery : IRequest<GetTripByIdResponse>
    {
        public int TripId { get; set; }
    }
}