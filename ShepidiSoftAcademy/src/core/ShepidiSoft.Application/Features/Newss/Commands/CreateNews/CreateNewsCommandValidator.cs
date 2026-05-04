using FluentValidation;
using ShepidiSoft.Application.Features.Newss.Commands.CreateNewss;

namespace ShepidiSoft.Application.Features.Newss.Commands.CreateNews;

public sealed class CreateNewsCommandValidator : AbstractValidator<CreateNewsCommand>
{
    public CreateNewsCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Haber başlığı boş olamaz.")
            .MinimumLength(3).WithMessage("Haber başlığı en az 3 karakter olmalıdır.")
            .MaximumLength(200).WithMessage("Haber başlığı 200 karakterden uzun olamaz.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Haber içeriği boş olamaz.")
            .MinimumLength(10).WithMessage("Haber içeriği çok kısa, lütfen biraz daha detay ekleyin.");

        RuleFor(x => x.Summary)
            .NotEmpty().WithMessage("Haber özeti boş olamaz.")
            .MaximumLength(500).WithMessage("Özet alanı 500 karakteri geçmemelidir.");

        // URL format kontrolü (Eğer URL dolu gelirse doğrula)
        RuleFor(x => x.ThumbnailUrl)
            .Must(BeAValidUrl).WithMessage("Geçerli bir Thumbnail URL adresi giriniz.")
            .When(x => !string.IsNullOrEmpty(x.ThumbnailUrl));

        RuleFor(x => x.BannerUrl)
            .Must(BeAValidUrl).WithMessage("Geçerli bir Banner URL adresi giriniz.")
            .When(x => !string.IsNullOrEmpty(x.BannerUrl));
    }

    private bool BeAValidUrl(string? url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
