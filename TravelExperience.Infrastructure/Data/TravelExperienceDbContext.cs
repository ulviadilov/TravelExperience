using Microsoft.EntityFrameworkCore;
using TravelExperience.Domain.Models;

namespace TravelExperience.Infrastructure.Data;
public class TravelExperienceDbContext : DbContext
{
    public TravelExperienceDbContext(DbContextOptions<TravelExperienceDbContext> options) : base(options) { }

    public DbSet<Trip> Trips { get; set; } = null!;
    public DbSet<Activity> Activities { get; set; } = null!;
    public DbSet<Destination> Destinations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TotalCost).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UserId).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Cost).HasColumnType("decimal(18,2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(e => e.Trip)
                .WithMany(t => t.Activities)
                .HasForeignKey(e => e.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Destination)
                .WithMany(d => d.Activities)
                .HasForeignKey(e => e.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Destination>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasIndex(e => e.Name);
            entity.HasIndex(e => new { e.Country });
        });
    }
}
