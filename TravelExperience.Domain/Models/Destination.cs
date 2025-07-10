namespace TravelExperience.Domain.Models;
public class Destination : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
}