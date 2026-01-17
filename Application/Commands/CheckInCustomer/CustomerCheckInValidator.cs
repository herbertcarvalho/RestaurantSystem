using FluentValidation;

namespace Application.Commands.CheckInCustomer;

public class CustomerCheckInValidator : AbstractValidator<CustomerCheckInCommand>
{
    public CustomerCheckInValidator()
    {
        RuleFor(r => r.Notes)
            .NotEmpty()
            .WithMessage("Notes is required")
            .MaximumLength(200)
            .WithMessage("Notes needs to be less than 200");
    }
}
