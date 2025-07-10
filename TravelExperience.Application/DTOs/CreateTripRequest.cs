namespace TravelExperience.Application.DTOs;
public class CreateTripRequest
{
    public string Title { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<CreateActivityDto> Activities { get; set; } = new List<CreateActivityDto>();
}