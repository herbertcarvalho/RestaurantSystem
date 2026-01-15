using FluentValidation;

namespace Application.Commands.CreateReservation;

public class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationValidator()
    {
        RuleFor(r => r.CustomerName)
            .NotEmpty().WithMessage("Customer name is required")
            .MaximumLength(200);
        RuleFor(r => r.CustomerEmail)
            .NotEmpty()
            .EmailAddress();
        RuleFor(r => r.CustomerPhone)
            .NotEmpty()
            .Matches(@"^\d{10,11}$").WithMessage("Phone must be 10 or 11 digits");
        RuleFor(r => r.NumberOfGuests)
            .GreaterThan(0)
            .LessThanOrEqualTo(20);
        RuleFor(r => r.ReservationDate)
            .GreaterThan(DateTime.UtcNow.AddHours(2))
            .WithMessage("Reservation must be at least 2 hours in advance");
    }

}