using TravelExperience.Application.Interfaces;
using TravelExperience.Domain.Core.Trip;
using TravelExperience.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelExperience.Application.Services
{
    public class TripService : ITripService
    {
        private readonly ITripWriteRepository _tripWriteRepository;
        private readonly ITripReadRepository _tripReadRepository;

        public TripService(ITripWriteRepository tripWriteRepository, ITripReadRepository tripReadRepository)
        {
            _tripWriteRepository = tripWriteRepository;
            _tripReadRepository = tripReadRepository;
        }

        public async Task<Trip> CreateAsync(Trip trip)
        {
            return await _tripWriteRepository.CreateAsync(trip);
        }

        public async Task<Trip?> GetByIdAsync(int tripId)
        {
            return await _tripReadRepository.GetWhere(t => t.TripId == tripId, tracking: false)
                .Include(t => t.Activities)
                .ThenInclude(a => a.Destination)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Trip>> GetByUserIdAsync(string userId)
        {
            return await _tripReadRepository.GetWhere(t => t.UserId == userId, tracking: false)
                .Include(t => t.Activities)
                .ThenInclude(a => a.Destination)
                .ToListAsync();
        }
    }
}