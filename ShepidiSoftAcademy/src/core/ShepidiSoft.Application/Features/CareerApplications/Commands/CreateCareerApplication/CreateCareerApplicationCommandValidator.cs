using FluentValidation;
using ShepidiSoft.Application.Features.CareerApplications.Commands.CreateCareerApplication;

public sealed class CreateCareerApplicationCommandValidator : AbstractValidator<CreateCareerApplicationCommand>
{
    public CreateCareerApplicationCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad alanı boş bırakılamaz.")
            .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad alanı boş bırakılamaz.")
            .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta alanı boş bırakılamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
            .MaximumLength(150).WithMessage("E-posta en fazla 150 karakter olabilir.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[0-9\s\-\(\)]{7,20}$").WithMessage("Geçerli bir telefon numarası giriniz.")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

        RuleFor(x => x.LinkedInUrl)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
                         uri.Host.Contains("linkedin.com"))
            .WithMessage("Geçerli bir LinkedIn URL'i giriniz.")
            .When(x => !string.IsNullOrEmpty(x.LinkedInUrl));

        RuleFor(x => x.GithubUrl)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
                         uri.Host.Contains("github.com"))
            .WithMessage("Geçerli bir GitHub URL'i giriniz.")
            .When(x => !string.IsNullOrEmpty(x.GithubUrl));

        RuleFor(x => x.CoverLetter)
            .NotEmpty().WithMessage("Ön yazı alanı boş bırakılamaz.")
            .MinimumLength(50).WithMessage("Ön yazı en az 50 karakter olmalıdır.")
            .MaximumLength(2000).WithMessage("Ön yazı en fazla 2000 karakter olabilir.");

        RuleFor(x => x.CvUrl)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("Geçerli bir CV URL'i giriniz.")
            .When(x => !string.IsNullOrEmpty(x.CvUrl));

        RuleFor(x => x.OrganizationPositionId)
            .GreaterThan(0).WithMessage("Geçerli bir pozisyon seçiniz.");
    }
}