using FluentValidation;

namespace ShepidiSoft.Application.Features.Documents.Commands.CreateDocument;

public sealed class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
{
    public CreateDocumentCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Başlık zorunludur.")
            .MaximumLength(200)
            .WithMessage("Başlık 200 karakteri geçemez.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Açıklama zorunludur.")
            .MaximumLength(1000)
            .WithMessage("Açıklama 1000 karakteri geçemez.");
    }
}
