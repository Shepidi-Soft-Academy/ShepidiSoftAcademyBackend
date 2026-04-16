using FluentValidation;

namespace ShepidiSoft.Application.Features.Courses.Commands.DeleteCourse;

public sealed class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
{
    public DeleteCourseCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Course Id must be greater than 0");
    }
}
