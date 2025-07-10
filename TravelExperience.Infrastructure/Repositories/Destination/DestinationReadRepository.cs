using TravelExperience.Domain.Core.Destination;
using TravelExperience.Infrastructure.Data;

namespace TravelExperience.Infrastructure.Repositories.Destination
{
    public class DestinationReadRepository : ReadRepository<Domain.Models.Destination>, IDestinationReadRepository
    {
        public DestinationReadRepository(TravelExperienceDbContext context) : base(context)
        {
        }
    }
}