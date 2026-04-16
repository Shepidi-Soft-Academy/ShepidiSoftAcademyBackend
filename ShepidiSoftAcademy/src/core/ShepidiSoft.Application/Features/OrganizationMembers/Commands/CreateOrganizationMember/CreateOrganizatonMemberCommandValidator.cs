using FluentValidation;
using ShepidiSoft.Application.Features.OrganizationMembers.Commands.CreateOrganizationMember;
using ShepidiSoft.Application.Features.Users.Validators;

public sealed class CreateOrganizationMemberCommandValidator
    : AbstractValidator<CreateOrganizationMemberCommand>
{
    public CreateOrganizationMemberCommandValidator()
    {
        RuleFor(x => x)
            .Must(x => x.UserId.HasValue ^ x.CreateUserRequest != null)
            .WithMessage("Ya mevcut kullanıcı seçilmeli ya da yeni kullanıcı bilgileri girilmeli, ikisi birden olamaz.");

        When(x => x.CreateUserRequest != null, () =>
        {
            RuleFor(x => x.CreateUserRequest!)
                .SetValidator(new CreateUserRequestValidator());
        });

        When(x => x.UserId.HasValue, () =>
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("Geçerli bir UserId girilmelidir.");
        });

        RuleFor(x => x.PositionIds)
            .NotEmpty()
            .WithMessage("En az 1 pozisyon seçilmelidir.")
            .Must(ids => ids.Distinct().Count() == ids.Count)
            .WithMessage("Aynı pozisyon birden fazla eklenemez.");

        RuleForEach(x => x.PositionIds)
            .GreaterThan(0)
            .WithMessage("Geçersiz PositionId.");
    }
}