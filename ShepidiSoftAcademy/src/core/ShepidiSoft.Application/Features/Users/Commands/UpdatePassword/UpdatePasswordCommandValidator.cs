using FluentValidation;

namespace ShepidiSoft.Application.Features.Users.Commands.UpdatePassword;

public sealed class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("Eski şifre boş bırakılamaz.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Yeni şifre boş bırakılamaz.")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
            .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
            .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
            .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.")
            .NotEqual(x => x.OldPassword).WithMessage("Yeni şifre eski şifreden farklı olmalıdır.");

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty().WithMessage("Şifre tekrarı boş bırakılamaz.")
            .Equal(x => x.NewPassword).WithMessage("Şifreler uyuşmuyor.");
    }
}