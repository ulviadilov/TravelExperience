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
                .WithMessage("Destination ID must be greater than 0")
                .LessThanOrEqualTo(int.MaxValue)
                .WithMessage("Destination ID is invalid");

            RuleFor(x => x.Duration)
                .GreaterThan(0)
                .WithMessage("Duration must be greater than 0")
                .LessThanOrEqualTo(24)
                .WithMessage("Activity duration cannot exceed 24 hours");

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Cost cannot be negative")
                .LessThanOrEqualTo(50000)
                .WithMessage("Activity cost cannot exceed $50,000");
        }
    }
}