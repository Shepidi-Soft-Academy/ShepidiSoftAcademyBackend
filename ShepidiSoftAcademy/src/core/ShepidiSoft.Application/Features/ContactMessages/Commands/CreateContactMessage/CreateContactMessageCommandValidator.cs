using FluentValidation;

namespace ShepidiSoft.Application.Features.ContactMessages.Commands.CreateContactMessage;

using FluentValidation;

public class CreateContactMessageCommandValidator : AbstractValidator<CreateContactMessageCommand>
{
    public CreateContactMessageCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("İsim alanı boş olamaz.")
            .MaximumLength(100).WithMessage("İsim 100 karakterden uzun olamaz.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email alanı boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("Telefon numarası 20 karakterden uzun olamaz.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Mesaj içeriği boş olamaz.")
            .MaximumLength(1000).WithMessage("Mesaj 1000 karakterden uzun olamaz.");
    }
}