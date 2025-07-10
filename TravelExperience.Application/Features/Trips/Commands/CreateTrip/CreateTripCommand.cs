using MediatR;
using TravelExperience.Application.DTOs;

namespace TravelExperience.Application.Features.Trips.Commands.CreateTrip;
public class CreateTripCommand : IRequest<CreateTripResponse>
{
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<CreateActivityDto> Activities { get; set; } = new();
}