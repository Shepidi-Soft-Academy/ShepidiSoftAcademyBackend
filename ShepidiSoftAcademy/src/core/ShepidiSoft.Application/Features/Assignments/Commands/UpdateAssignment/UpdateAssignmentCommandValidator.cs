using FluentValidation;

namespace ShepidiSoft.Application.Features.Assignments.Commands.UpdateAssignment;

public sealed class UpdateAssignmentCommandValidator:AbstractValidator<UpdateAssignmentCommand>
{
    public UpdateAssignmentCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MaximumLength(2000).WithMessage("Açıklama en fazla 2000 karakter olabilir.");

        RuleFor(x => x.CourseId)
            .GreaterThan(0).WithMessage("Geçerli bir kurs seçilmelidir.");

        RuleFor(x => x.DueDate)
            .Must(BeAValidFutureDate)
            .WithMessage("Teslim tarihi geçmiş bir tarih olamaz.");
    }

    private bool BeAValidFutureDate(DateTime dueDate)
    {
        return dueDate >= DateTime.UtcNow.Date;
    }
}
