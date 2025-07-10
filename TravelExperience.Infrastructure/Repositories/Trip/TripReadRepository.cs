using Microsoft.EntityFrameworkCore;
using TravelExperience.Domain.Core.Trip;
using TravelExperience.Infrastructure.Data;

namespace TravelExperience.Infrastructure.Repositories.Trip;
public class TripReadRepository : ReadRepository<Domain.Models.Trip>, ITripReadRepository
{
    public TripReadRepository(TravelExperienceDbContext context) : base(context)
    {
    }
}