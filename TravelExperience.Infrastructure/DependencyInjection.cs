using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelExperience.Domain.Core.Destination;
using TravelExperience.Domain.Core.Trip;
using TravelExperience.Infrastructure.Data;
using TravelExperience.Infrastructure.Repositories.Destination;
using TravelExperience.Infrastructure.Repositories.Trip;

namespace TravelExperience.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddDbContext<TravelExperienceDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITripReadRepository, TripReadRepository>();
        services.AddScoped<ITripWriteRepository, TripWriteRepository>();
        services.AddScoped<IDestinationReadRepository, DestinationReadRepository>();


        return services;
    }
}