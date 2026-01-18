using FluentValidation;

namespace Application.Commands.CancelReservation;

public class CancelReservationValidator : AbstractValidator<CancelReservationCommand>
{
    public CancelReservationValidator()
    {
        RuleFor(r => r.Reason)
            .NotEmpty()
            .WithMessage("Reason is required")
            .MaximumLength(200)
            .WithMessage("Reason needs to be less than 200");

        RuleFor(r => r.CancelledBy)
            .NotEmpty()
            .WithMessage("CancelledBy is required")
            .MaximumLength(200)
            .WithMessage("CancelledBy needs to be less than 200");
    }
}

