using FluentValidation;

namespace Application.Commands.CreateReservation;

public class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationValidator()
    {
        //mandatory
        RuleFor(r => r.CustomerName)
            .NotEmpty()
            .WithMessage("Customer name is required")
            .MaximumLength(200)
            .WithMessage("Customer name needs to be less than 200");

        RuleFor(r => r.CustomerEmail)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("E-mail needs to be valid");

        RuleFor(r => r.CustomerPhone)
            .NotEmpty()
            .Matches(@"^\d{10,11}$")
            .WithMessage("Phone must be 10 or 11 digits");

        RuleFor(r => r.ReservationDate)
            .GreaterThan(DateTime.UtcNow.AddHours(2))
            .WithMessage("Reservation must be at least 2 hours in advance");

        RuleFor(r => r.NumberOfGuests)
            .GreaterThan(0)
            .LessThanOrEqualTo(20)
            .WithMessage("Number of guests must be between 1 and 20.");

        RuleFor(r => r.RestaurantId)
            .GreaterThan(0)
            .WithMessage("The restaurant id needs to be valid");

        //optionals

        RuleFor(r => r.SpecialRequests)
            .Must(x => x is null || !string.IsNullOrEmpty(x))
            .WithMessage("Special requests needs to be valid");

        RuleFor(r => r.DepositAmount)
            .GreaterThan(0)
            .WithMessage("Deposit amount must be greater than 0");
    }

}