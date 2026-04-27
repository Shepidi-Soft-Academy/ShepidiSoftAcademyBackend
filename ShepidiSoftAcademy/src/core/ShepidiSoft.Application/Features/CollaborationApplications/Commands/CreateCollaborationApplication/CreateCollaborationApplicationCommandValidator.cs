using FluentValidation;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Commands.CreateCollaborationApplication;

public sealed class CreateCollaborationApplicationCommandValidator : AbstractValidator<CreateCollaborationApplicationCommand>
{
    public CreateCollaborationApplicationCommandValidator()
    {
        RuleFor(x => x.Title)
           .NotEmpty()
           .MaximumLength(100)
           .WithMessage("Başlık Boş Bırakılamaz!");
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000)
            .WithMessage("Açıklama Boş Bırakılamaz!");
        RuleFor(x => x.CommunityName)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Topluluk Adı Boş Bırakılamaz!");
        RuleFor(x => x.ContactName)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Ad Soyad Boş Bırakılamaz!");
        RuleFor(x => x.ContactEmail)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255)
            .WithMessage("Email Boş Bırakılamaz!");
        RuleFor(x => x.ContactPhone)
            .NotEmpty()
            .Matches(@"^\+?[1-9]\d{1,14}$") 
            .WithMessage("Contact phone must be a valid phone number in E.164 format");

    }
}
