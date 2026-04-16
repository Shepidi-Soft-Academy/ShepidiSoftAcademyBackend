using FluentValidation;

namespace ShepidiSoft.Application.Features.Offerings.Commands.CreateOffering;

public sealed class CreateOfferingCommandValidator : AbstractValidator<CreateOfferingCommand>
{
    public CreateOfferingCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş bırakılamaz.")
            .Length(3, 200).WithMessage("Başlık 3 ile 200 karakter arasında olmalıdır.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş bırakılamaz.")
            .Length(10, 2000).WithMessage("Açıklama 10 ile 2000 karakter arasında olmalıdır.");

        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("Aktif durumu belirtilmelidir.");
    }
}
