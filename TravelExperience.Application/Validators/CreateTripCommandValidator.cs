using FluentValidation;
using TravelExperience.Application.Features.Trips.Commands.CreateTrip;

namespace TravelExperience.Application.Validators
{
    public class CreateTripCommandValidator : AbstractValidator<CreateTripCommand>
    {
        public CreateTripCommandValidator()
        {
            RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required")
            .MaximumLength(50)
            .WithMessage("User ID cannot exceed 50 characters");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(200)
                .WithMessage("Title cannot exceed 200 characters");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required")
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Start date cannot be in the past");

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("End date is required")
                .GreaterThan(x => x.StartDate)
                .WithMessage("End date must be after start date");

            RuleFor(x => x.Activities)
                .NotEmpty()
                .WithMessage("At least one activity is required");

            RuleForEach(x => x.Activities).SetValidator(new CreateActivityDtoValidator());
        }
    }
}