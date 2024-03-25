

using DataLawyer.Application.Processes.Commands;
using FluentValidation;

namespace DataLawyer.Application.Validators;

public class ProcessUpdateCommandValidator : AbstractValidator<ProcessUpdateCommand>
{
    public ProcessUpdateCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThanOrEqualTo(0).WithMessage("Invalid process Id.");

        RuleFor(x => x.ProcessNumber).NotEmpty().WithMessage("Process number is required.");

        RuleFor(x => x.Situation).NotEmpty().WithMessage("Situation is required.");

        RuleFor(x => x.Grade).NotEmpty().WithMessage("Situation is required.");

        RuleFor(x => x.Area.Description).NotEmpty().WithMessage("Description of the area is required.");

        RuleFor(x => x.Topic).NotEmpty().WithMessage("Topic is required.");

        RuleFor(x => x.From).NotEmpty().WithMessage("From is required.");

        RuleFor(x => x.Distribution).NotEmpty().WithMessage("Distribution is required.");

        RuleFor(x => x.Rapporteur).NotEmpty().WithMessage("Rapporteur is required.");

        RuleForEach(x => x.Movements).ChildRules(order =>
        {
            order.RuleFor(w => w.Id).GreaterThanOrEqualTo(0).WithMessage("Invalid movement id.");
            order.RuleFor(w => w.TheMovement).NotEmpty().WithMessage("Movement is required.");
            order.RuleFor(w => w.DateMovement).NotEmpty().WithMessage("Date of the movement is required.");
            
        });
    }
}
