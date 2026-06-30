using FluentValidation;

namespace ShepidiSoft.Application.Features.Organizations.Commands.UpdateOrganization;

public sealed class UpdateOrganizationCommandValidator
    : AbstractValidator<UpdateOrganizationCommand>
{
    public UpdateOrganizationCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.LogoUrl)
            .MaximumLength(1000);

        RuleFor(x => x.LinkedInUrl)
            .MaximumLength(1000);

        RuleFor(x => x.InstagramUrl)
            .MaximumLength(1000);

        RuleFor(x => x.WhatsappUrl)
            .MaximumLength(1000);

        RuleFor(x => x.YoutubeUrl)
            .MaximumLength(1000);
    }
}