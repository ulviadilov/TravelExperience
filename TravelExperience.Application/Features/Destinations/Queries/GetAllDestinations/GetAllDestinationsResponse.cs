using TravelExperience.Application.DTOs;

namespace TravelExperience.Application.Features.Destinations.Queries.GetAllDestinations
{
    public class GetAllDestinationsResponse
    {
        public List<DestinationDto> Destinations { get; set; } = new();
    }
}