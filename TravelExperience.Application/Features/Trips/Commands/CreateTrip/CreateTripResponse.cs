using TravelExperience.Application.DTOs;

namespace TravelExperience.Application.Features.Trips.Commands.CreateTrip
{
    public class CreateTripResponse
    {
        public int TripId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ActivityDto> Activities { get; set; } = new();
    }
}