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
                .Length(8, 32)
                .WithMessage("User ID must be between 8 and 32 characters")
                .Matches(@"^[a-zA-Z0-9]+$")
                .WithMessage("User ID can only contain letters and numbers");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(200)
                .WithMessage("Title cannot exceed 200 characters");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required")
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Start date cannot be in the past")
                .LessThanOrEqualTo(DateTime.Today.AddYears(10))
                .WithMessage("Start date cannot be more than 10 years in the future");

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("End date is required")
                .GreaterThan(x => x.StartDate)
                .WithMessage("End date must be after start date")
                .LessThanOrEqualTo(DateTime.Today.AddYears(10))
                .WithMessage("End date cannot be more than 10 years in the future");

            RuleFor(x => x)
                .Must(x => x.EndDate.Subtract(x.StartDate).Days <= 365)
                .WithMessage("Trip duration cannot exceed 365 days")
                .When(x => x.StartDate != default && x.EndDate != default);

            RuleFor(x => x.Activities)
                .NotEmpty()
                .WithMessage("At least one activity is required")
                .Must(activities => activities.Count <= 50)
                .WithMessage("Cannot have more than 50 activities per trip");

            RuleForEach(x => x.Activities).SetValidator(new CreateActivityDtoValidator());
        }
    }
}