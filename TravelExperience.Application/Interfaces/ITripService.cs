using TravelExperience.Domain.Models;

namespace TravelExperience.Application.Interfaces;
public interface ITripService
{
    Task<Trip?> GetByIdAsync(int tripId);
    Task<List<Trip>> GetByUserIdAsync(string userId);
    Task<Trip> CreateAsync(Trip trip);
}
