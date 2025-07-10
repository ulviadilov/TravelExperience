using FluentValidation;
using TravelExperience.Application.DTOs;

namespace TravelExperience.Application.Validators
{
    public class CreateActivityDtoValidator : AbstractValidator<CreateActivityDto>
    {
        public CreateActivityDtoValidator()
        {
            RuleFor(x => x.DestinationId)
                .GreaterThan(0)
                .WithMessage("Destination ID must be greater than 0");

            RuleFor(x => x.Duration)
                .GreaterThan(0)
                .WithMessage("Duration must be greater than 0");

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Cost cannot be negative");
        }
    }
}