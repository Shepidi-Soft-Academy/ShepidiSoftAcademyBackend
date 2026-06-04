using FluentValidation;

namespace ShepidiSoft.Application.Features.Documents.Commands.CreateDocument;

public sealed class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
{
    public CreateDocumentCommandValidator()
    {
    }
}
