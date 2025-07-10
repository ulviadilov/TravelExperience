using TravelExperience.Domain.Models;
namespace TravelExperience.Application.Interfaces
{
    public interface IDestinationService
    {
        Task<IEnumerable<Destination>> GetAllAsync();
    }
}