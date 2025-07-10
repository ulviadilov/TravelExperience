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
                .Length(8, 32)
                .WithMessage("User ID must be between 8 and 32 characters")
                .Matches(@"^[a-zA-Z0-9]+$")
                .WithMessage("User ID can only contain letters and numbers");
        }
    }
}