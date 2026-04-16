using FluentValidation;

namespace ShepidiSoft.Application.Features.Instructors.Commands.UpdateInstructor;

public sealed class UpdateInstructorCommandValidator : AbstractValidator<UpdateInstructorCommand>
{
    public UpdateInstructorCommandValidator()
    {
        RuleFor(x => x.Bio)
            .NotEmpty().WithMessage("Biyografi boş bırakılamaz.")
            .Length(10, 1000).WithMessage("Biyografi 10 ile 1000 karakter arasında olmalıdır.");

        RuleFor(x => x.Expertise)
            .NotEmpty().WithMessage("Uzmanlık alanı boş bırakılamaz.")
            .Length(3, 500).WithMessage("Uzmanlık alanı 3 ile 500 karakter arasında olmalıdır.");

        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("Aktif durumu belirtilmelidir.");
    }
}
