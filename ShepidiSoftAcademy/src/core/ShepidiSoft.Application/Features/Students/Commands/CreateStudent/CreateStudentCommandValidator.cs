using FluentValidation;
using ShepidiSoft.Application.Features.Users.Validators;

namespace ShepidiSoft.Application.Features.Students.Commands.CreateStudent;

public sealed class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(x => x.University)
            .NotEmpty().WithMessage("Üniversite adı boş bırakılamaz.")
            .Length(2, 200).WithMessage("Üniversite adı 2 ile 200 karakter arasında olmalıdır.");

        RuleFor(x => x.Department)
            .NotEmpty().WithMessage("Bölüm adı boş bırakılamaz.")
            .Length(2, 200).WithMessage("Bölüm adı 2 ile 200 karakter arasında olmalıdır.");

        RuleFor(x => x.CreateUserCommand)
            .NotNull().WithMessage("Kullanıcı bilgileri boş bırakılamaz.")
            .SetValidator(new CreateUserRequestValidator());
    }
}
