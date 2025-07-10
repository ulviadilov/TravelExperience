namespace TravelExperience.Application.DTOs;
public class ActivityDto
{
    public int ActivityId { get; set; }
    public int DestinationId { get; set; }
    public int Duration { get; set; }
    public decimal Cost { get; set; }
    public DateTime CreatedAt { get; set; }
}