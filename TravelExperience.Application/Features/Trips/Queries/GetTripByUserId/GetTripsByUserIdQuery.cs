using MediatR;

namespace TravelExperience.Application.Features.Trips.Queries.GetTripByUserId
{
    public class GetTripsByUserIdQuery : IRequest<GetTripsByUserIdResponse>
    {
        public string UserId { get; set; } = string.Empty;
    }
}