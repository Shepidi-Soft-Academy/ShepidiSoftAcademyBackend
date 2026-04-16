using FluentValidation;

namespace ShepidiSoft.Application.Features.Offerings.Queries.GetOfferingDetail;

public sealed class GetOfferingDetailQueryValidator : AbstractValidator<GetOfferingDetailQuery>
{
    public GetOfferingDetailQueryValidator()
    {
        RuleFor(x => x.Id)
          .GreaterThan(0)
          .WithMessage("Id 0'dan büyük olmalıdır.");
    }
}
