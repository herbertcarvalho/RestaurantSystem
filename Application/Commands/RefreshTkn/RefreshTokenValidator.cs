using FluentValidation;

namespace Application.Commands.RefreshTkn;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidator()
    {
        RuleFor(r => r.RefreshToken)
           .NotEmpty()
           .WithMessage("RefreshToken is required.")
           .MaximumLength(200)
           .WithMessage("RefreshToken needs to be less than 200");
    }
}