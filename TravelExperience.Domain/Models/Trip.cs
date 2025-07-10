namespace TravelExperience.Domain.Models;
public class Trip : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalCost { get; set; }
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}