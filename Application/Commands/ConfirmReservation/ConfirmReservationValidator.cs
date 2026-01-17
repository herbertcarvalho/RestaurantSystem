using FluentValidation;

namespace Application.Commands.ConfirmReservation;

public class ConfirmReservationValidator : AbstractValidator<ConfirmReservationCommand>
{
    public ConfirmReservationValidator()
    {
        RuleFor(r => r.ConfirmedBy)
            .NotEmpty()
            .WithMessage("Confirmed by is required")
            .MaximumLength(200)
            .WithMessage("Confirmed by needs to be less than 200");

        RuleFor(r => r.Notes)
            .NotEmpty()
            .WithMessage("Notes is required")
            .MaximumLength(200)
            .WithMessage("Notes needs to be less than 200");
    }
}
public class ConfirmReservationWebhookValidator : AbstractValidator<ConfirmReservationWebhookCommand>
{
    public ConfirmReservationWebhookValidator()
    {
        RuleFor(r => r.TransactionId)
            .NotEmpty()
            .WithMessage("Transaction id is required")
            .MaximumLength(200)
            .WithMessage("Transaction id name needs to be less than 200");

        RuleFor(r => r.ReservationCode)
            .NotEmpty()
            .WithMessage("Reservation code is required")
            .MaximumLength(200)
            .WithMessage("Reservation code needs to be less than 200");

        RuleFor(r => r.Amount)
           .GreaterThan(0)
           .WithMessage("Deposit amount must be greater than 0");

        RuleFor(r => r.Status)
            .Must(x => x == "APPROVED")
            .WithMessage("The payment is not approved.");
    }
}

