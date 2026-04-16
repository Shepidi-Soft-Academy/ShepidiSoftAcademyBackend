using FluentValidation;

namespace ShepidiSoft.Application.Features.Activities.Commands.DeleteActivity;

public sealed class DeleteActivityCommandValidator : AbstractValidator<DeleteActivityCommand>
{
    public DeleteActivityCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Activity Id must be greater than 0");
    }
}
