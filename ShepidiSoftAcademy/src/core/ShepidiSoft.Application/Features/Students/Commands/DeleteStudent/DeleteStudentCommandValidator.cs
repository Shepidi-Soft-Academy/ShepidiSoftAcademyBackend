using FluentValidation;

namespace ShepidiSoft.Application.Features.Students.Commands.DeleteStudent;

public sealed class DeleteStudentCommandValidator : AbstractValidator<DeleteStudentCommand>
{
    public DeleteStudentCommandValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty() 
           .WithMessage("Id boş olmamalı");
    }
}
