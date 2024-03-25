
using DataLawyer.Application.Processes.Commands;
using FluentValidation;

namespace DataLawyer.Application.Validators;

public class ProcessCreateCommandValidator : AbstractValidator<ProcessCreateCommand>
{
    public ProcessCreateCommandValidator()
    {
        RuleFor(x => x.Process).NotEmpty().WithMessage("Process number is required.");
    }
}
