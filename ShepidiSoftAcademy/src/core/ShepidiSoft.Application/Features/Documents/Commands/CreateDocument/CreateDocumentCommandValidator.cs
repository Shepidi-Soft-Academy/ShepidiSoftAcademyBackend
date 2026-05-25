using FluentValidation;

namespace ShepidiSoft.Application.Features.Documents.Commands.CreateDocument;

public sealed class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
{
    public CreateDocumentCommandValidator()
    {
        RuleFor(x => x.DocumentTopicId)
            .GreaterThan(0).WithMessage("Konu başlığı seçilmelidir.");
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");
        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Açıklama en fazla 1000 karakter olabilir.");
        RuleFor(x => x.FileUrl)
            .NotEmpty().WithMessage("Dosya URL'si boş olamaz.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Geçerli bir dosya URL'si girilmelidir.");
    }
}
