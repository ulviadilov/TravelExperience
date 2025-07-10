using MediatR;
using TravelExperience.Application.DTOs;
using TravelExperience.Application.Interfaces;

namespace TravelExperience.Application.Features.Destinations.Queries.GetAllDestinations
{
    public class GetAllDestinationsQueryHandler : IRequestHandler<GetAllDestinationsQuery, GetAllDestinationsResponse>
    {
        private readonly IDestinationService _destinationService;

        public GetAllDestinationsQueryHandler(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        /// <summary>
        /// Handles the query to retrieve all available destinations.
        /// </summary>
        /// <param name="request">The query to fetch all destinations.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A response containing a list of all destinations.</returns>
        public async Task<GetAllDestinationsResponse> Handle(GetAllDestinationsQuery request, CancellationToken cancellationToken)
        {
            var destinations = await _destinationService.GetAllAsync();

            return new GetAllDestinationsResponse
            {
                Destinations = destinations.Select(d => new DestinationDto
                {
                    DestinationId = d.Id,
                    Name = d.Name,
                    Country = d.Country,
                    CreatedAt = d.CreatedAt,
                    UpdatedAt = d.UpdatedAt
                }).ToList()
            };
        }
    }
}