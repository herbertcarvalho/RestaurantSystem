using FluentValidation;

namespace Application.Commands.CompleteReservation;

public class CompleteReservationValidator : AbstractValidator<CompleteReservationCommand>
{
    public CompleteReservationValidator()
    {
    }
}