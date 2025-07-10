using TravelExperience.Application.Features.Trips.Queries.GetTripById;

namespace TravelExperience.Application.Features.Trips.Queries.GetTripByUserId
{
    public class GetTripsByUserIdResponse
    {
        public List<GetTripByIdResponse> Trips { get; set; } = new();
    }
}