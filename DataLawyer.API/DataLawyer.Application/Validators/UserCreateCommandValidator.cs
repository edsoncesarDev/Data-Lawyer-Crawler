
using DataLawyer.Application.Users.Commands;
using FluentValidation;

namespace DataLawyer.Application.Validators;

public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
{
    public UserCreateCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThanOrEqualTo(0).WithMessage("Invalid user Id.");

        RuleFor(v => v.Name).NotEmpty().WithMessage("Name required.");

        RuleFor(v => v.Email).NotEmpty().EmailAddress().WithMessage("Invalid email.");

        RuleFor(v => v.Password).NotEmpty().Length(8, 20).WithMessage("Invalid password.");
    }
}
