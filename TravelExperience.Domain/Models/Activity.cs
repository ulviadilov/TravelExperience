namespace TravelExperience.Domain.Models;
public class Activity
{
    public int ActivityId { get; set; }
    public int TripId { get; set; }
    public int DestinationId { get; set; }
    public int Duration { get; set; }
    public decimal Cost { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Trip Trip { get; set; } = null!;
    public virtual Destination Destination { get; set; } = null!;
}