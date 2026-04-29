using FluentValidation;

namespace ShepidiSoft.Application.Features.StudentRequests.Commands.CreateStudentRequest;

public sealed class CreateStudentRequestCommandValidator : AbstractValidator<CreateStudentRequestCommand>
{
    public CreateStudentRequestCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş bırakılamaz.")
            .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş bırakılamaz.")
            .MaximumLength(3000).WithMessage("Lütfen talebinizi daha detaylı açıklayın  en fazla 3000 karakter ).");
    }
}