using FluentValidation;
using TravelExperience.Application.Features.Trips.Queries.GetTripById;

namespace TravelExperience.Application.Validators
{
    public class GetTripByIdQueryValidator : AbstractValidator<GetTripByIdQuery>
    {
        public GetTripByIdQueryValidator()
        {
            RuleFor(x => x.TripId)
                .GreaterThan(0)
                .WithMessage("Trip ID must be greater than 0");
        }
    }
}