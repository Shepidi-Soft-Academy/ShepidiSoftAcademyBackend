using FluentValidation;

namespace ShepidiSoft.Application.Features.Assignments.Commands.DeleteAssignment;

public sealed class DeleteAssignmentCommandValidator : AbstractValidator<DeleteAssignmentCommand>
{
    public DeleteAssignmentCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0)
           .WithMessage("Activity Id must be greater than 0");
    }
}
