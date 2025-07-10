namespace TravelExperience.Domain.Models;
public class Destination
{
    public int DestinationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
}