using FluentValidation;

namespace ShepidiSoft.Application.Features.CommunityServices.Commands.UpdateCommunityService;

public sealed class UpdateCommunityServiceCommandValidator : AbstractValidator<UpdateCommunityServiceCommand>
{
    public UpdateCommunityServiceCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Hizmet ID'si 0'dan büyük olmalıdır.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Hizmet başlığı boş bırakılamaz.")
            .MinimumLength(3)
            .WithMessage("Hizmet başlığı en az 3 karakter olmalıdır.")
            .MaximumLength(200)
            .WithMessage("Hizmet başlığı en fazla 200 karakter olabilir.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Hizmet açıklaması boş bırakılamaz.")
            .MinimumLength(10)
            .WithMessage("Hizmet açıklaması en az 10 karakter olmalıdır.")
            .MaximumLength(2000)
            .WithMessage("Hizmet açıklaması en fazla 2000 karakter olabilir.");


        RuleFor(x => x.IsActive)
            .NotNull()
            .WithMessage("Aktif durumu belirtilmelidir.");

        
        RuleFor(x => x.ImageUrl)
            .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Geçerli bir resim URL'si giriniz.");
    }
}
