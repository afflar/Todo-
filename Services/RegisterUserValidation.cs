using FluentValidation;
using FluentValidation.Validators;
using System.Data;
using TodoList.Models;

namespace TodoList.Services;

public class RegisterUserValidation : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidation()
    {
        RuleFor(n => n.name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(4).WithMessage("Longer than 4")
            .MaximumLength(16).WithMessage("Shorter than 16");

        RuleFor(n => n.email)
            .NotEmpty().WithMessage("Email can't be blank")
            .EmailAddress().WithMessage("Invalid email form");

        RuleFor(n => n.password)
            .NotEmpty().WithMessage("Enter your password")
            .MinimumLength(4).WithMessage("Password must be longer than 4 and shorter than 16 symbols")
            .MaximumLength(16).WithMessage("Password must be longer than 4 and shorter than 16 symbols")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase, one lowercase letter and one symbol.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one uppercase, one lowercase letter and one symbol.")
            .Matches(@"[^A-Za-z0-9]").WithMessage("Password must contain at least one uppercase, one lowercase letter and one symbol.");
    }
}