using FluentValidation;

namespace Application.Commands.ReviewReservation;

public class ReviewReservationValidator : AbstractValidator<ReviewReservationCommand>
{
    public ReviewReservationValidator()
    {
        RuleFor(r => r.Rating)
            .GreaterThan(-1)
            .LessThanOrEqualTo(5)
            .WithMessage("Rating must be between 0 and 5.");

        RuleFor(r => r.Comment)
            .NotEmpty()
            .WithMessage("Comment is required")
            .MaximumLength(200)
            .WithMessage("Comment needs to be less than 200");

        RuleFor(r => r.Category)
           .NotEmpty()
           .WithMessage("Category is required")
           .MaximumLength(200)
           .WithMessage("Category needs to be less than 200");
    }
}
