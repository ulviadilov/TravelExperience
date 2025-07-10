using TravelExperience.Application.Interfaces;
using TravelExperience.Domain.Core.Destination;
using TravelExperience.Domain.Models;

namespace TravelExperience.Application.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationReadRepository _destinationReadRepository;

        public DestinationService(IDestinationReadRepository destinationReadRepository)
        {
            _destinationReadRepository = destinationReadRepository;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            return await _destinationReadRepository.GetAllAsync();
        }
    }
}