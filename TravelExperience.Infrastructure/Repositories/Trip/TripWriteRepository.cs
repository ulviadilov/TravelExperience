using TravelExperience.Domain.Core.Trip;
using TravelExperience.Infrastructure.Data;

namespace TravelExperience.Infrastructure.Repositories.Trip;
public class TripWriteRepository : WriteRepository<Domain.Models.Trip>, ITripWriteRepository
{
    public TripWriteRepository(TravelExperienceDbContext context) : base(context)
    {
    }
}