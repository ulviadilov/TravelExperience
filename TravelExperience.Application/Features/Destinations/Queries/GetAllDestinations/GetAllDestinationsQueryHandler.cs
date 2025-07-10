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
        public async Task<GetAllDestinationsResponse> Handle(GetAllDestinationsQuery request, CancellationToken cancellationToken)
        {
            var destinations = await _destinationService.GetAllAsync();

            return new GetAllDestinationsResponse
            {
                Destinations = destinations.Select(d => new DestinationDto
                {
                    DestinationId = d.DestinationId,
                    Name = d.Name,
                    Country = d.Country,
                    CreatedAt = d.CreatedAt,
                    UpdatedAt = d.UpdatedAt
                }).ToList()
            };
        }
    }
}