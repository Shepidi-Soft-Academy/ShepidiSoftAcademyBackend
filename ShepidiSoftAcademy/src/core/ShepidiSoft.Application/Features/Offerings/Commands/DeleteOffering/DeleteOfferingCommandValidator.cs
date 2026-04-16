using FluentValidation;

namespace ShepidiSoft.Application.Features.Offerings.Commands.DeleteOffering;

public sealed class DeleteOfferingCommandValidator : AbstractValidator<DeleteOfferingCommand>
{
    public DeleteOfferingCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0)
           .WithMessage("Activity Id must be greater than 0");
    }
}
