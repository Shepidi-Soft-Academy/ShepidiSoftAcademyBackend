using FluentValidation;
using ShepidiSoft.Application.Features.Newss.Commands.UpdateNewss;

namespace ShepidiSoft.Application.Features.Newss.Commands.UpdateNews;

public class UpdateNewsCommandValidator : AbstractValidator<UpdateNewsCommand>
{
    public UpdateNewsCommandValidator()
    {
        // ID kontrolü
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Güncellenecek haberin ID bilgisi boş olamaz.")
            .GreaterThan(0).WithMessage("Geçersiz haber ID.");

        // Başlık kuralları
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Haber başlığı boş bırakılamaz.")
            .MinimumLength(5).WithMessage("Haber başlığı en az 5 karakter olmalıdır.")
            .MaximumLength(200).WithMessage("Haber başlığı en fazla 200 karakter olabilir.");

        // İçerik kuralları
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Haber içeriği boş bırakılamaz.")
            .MinimumLength(20).WithMessage("Haber içeriği çok kısa, biraz daha detay ekleyin.");

        // Özet kuralları (Opsiyonel ama sınır konulmalı)
        RuleFor(x => x.Summary)
            .MaximumLength(500).WithMessage("Özet metni 500 karakterden fazla olamaz.");

        // URL format kontrolleri (Eğer link varsa)
        RuleFor(x => x.ThumbnailUrl)
            .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Geçersiz kapak görseli URL formatı.");
    }
}
