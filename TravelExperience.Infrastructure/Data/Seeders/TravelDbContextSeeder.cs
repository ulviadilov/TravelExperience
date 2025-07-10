using TravelExperience.Domain.Models;

namespace TravelExperience.Infrastructure.Data.Seeders
{
    public class TravelDbContextSeeder
    {
        public static async Task SeedAsync(TravelExperienceDbContext context)
        {
            if (!context.Destinations.Any())
            {
                var destinations = new List<Destination>
            {
                new Destination
                {
                    Name = "Paris",
                    Country = "France",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Name = "Rome",
                    Country = "Italy",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Name = "Barcelona",
                    Country = "Spain",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Name = "Amsterdam",
                    Country = "Netherlands",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Name = "London",
                    Country = "United Kingdom",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Name = "Prague",
                    Country = "Czech Republic",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Name = "Vienna",
                    Country = "Austria",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Name = "Berlin",
                    Country = "Germany",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Name = "Lisbon",
                    Country = "Portugal",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Destination
                {
                    Name = "Stockholm",
                    Country = "Sweden",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

                context.Destinations.AddRange(destinations);
                await context.SaveChangesAsync();
            }
        }
    }
}