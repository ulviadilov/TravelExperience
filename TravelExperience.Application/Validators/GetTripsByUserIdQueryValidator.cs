using FluentValidation;
using TravelExperience.Application.Features.Trips.Queries.GetTripByUserId;

namespace TravelExperience.Application.Validators
{
    public class GetTripsByUserIdQueryValidator : AbstractValidator<GetTripsByUserIdQuery>
    {
        public GetTripsByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required")
                .MaximumLength(50)
                .WithMessage("User ID cannot exceed 50 characters");
        }
    }
}