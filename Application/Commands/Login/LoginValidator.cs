using FluentValidation;

namespace Application.Commands.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("E-mail needs to be valid.");

        RuleFor(r => r.Password)
           .NotEmpty()
           .WithMessage("Password is required.")
           .MaximumLength(200)
           .WithMessage("Password needs to be less than 200");
    }
}
