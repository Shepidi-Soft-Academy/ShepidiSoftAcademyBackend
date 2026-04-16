using FluentValidation;

namespace ShepidiSoft.Application.Features.Instructors.Commands.DeleteInstructor;

public sealed class DeleteInstructorCommandValidator : AbstractValidator<DeleteInstructorCommand>
{
    public DeleteInstructorCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Activity Id must be greater than 0");
    }
}
