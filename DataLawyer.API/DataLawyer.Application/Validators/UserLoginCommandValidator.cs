
using DataLawyer.Application.Users.Commands;
using FluentValidation;

namespace DataLawyer.Application.Validators;

public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
{
    public UserLoginCommandValidator()
    {
        RuleFor(v => v.Email).NotEmpty().EmailAddress().WithMessage("Invalid email.");

        RuleFor(v => v.Password).NotEmpty().Length(8, 20).WithMessage("Invalid password.");
    }
}

