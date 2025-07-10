using FluentAssertions;
using Moq;
using TravelExperience.Application.DTOs;
using TravelExperience.Application.Features.Trips.Commands.CreateTrip;
using TravelExperience.Application.Interfaces;
using TravelExperience.Domain.Models;

namespace TravelExperience.Tests.Application.Handlers
{
    public class CreateTripCommandHandlerTests
    {
        private readonly Mock<ITripService> _tripServiceMock;
        private readonly CreateTripCommandHandler _handler;

        public CreateTripCommandHandlerTests()
        {
            _tripServiceMock = new Mock<ITripService>();
            _handler = new CreateTripCommandHandler(_tripServiceMock.Object);
        }

        [Theory]
        [InlineData("user12345", "Test Trip", 1, 5, 1, 100.50, 2, 200.75, 301.25, 2)]
        [InlineData("user67890", "Short Trip", 1, 2, 3, 50.00, 4, 75.25, 125.25, 2)]
        [InlineData("user11111", "One Activity Trip", 1, 3, 5, 150.00, 0, 0, 150.00, 1)]
        public async Task Handle_ValidCommand_ReturnsCorrectResponse(
            string userId, string title, int startDays, int endDays,
            int destinationId1, decimal cost1, int destinationId2, decimal cost2,
            decimal expectedTotal, int expectedActivityCount)
        {
            // Arrange
            var activities = new List<CreateActivityDto>();
            if (cost1 > 0) activities.Add(new CreateActivityDto
            {
                DestinationId = destinationId1,
                Duration = 2,
                Cost = cost1
            });
            if (cost2 > 0) activities.Add(new CreateActivityDto
            {
                DestinationId = destinationId2,
                Duration = 3,
                Cost = cost2
            });

            var command = new CreateTripCommand
            {
                UserId = userId,
                Title = title,
                StartDate = DateTime.UtcNow.AddDays(startDays),
                EndDate = DateTime.UtcNow.AddDays(endDays),
                Activities = activities
            };

            _tripServiceMock
                .Setup(x => x.CreateAsync(It.IsAny<Trip>()))
                .ReturnsAsync((Trip trip) => new Trip
                {
                    UserId = trip.UserId,
                    Title = trip.Title,
                    StartDate = trip.StartDate,
                    EndDate = trip.EndDate,
                    Activities = trip.Activities,
                    TotalCost = trip.Activities.Sum(a => a.Cost)
                });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.UserId.Should().Be(userId);
            result.Title.Should().Be(title);
            result.TotalCost.Should().Be(expectedTotal);
            result.Activities.Should().HaveCount(expectedActivityCount);
        }
    }
}