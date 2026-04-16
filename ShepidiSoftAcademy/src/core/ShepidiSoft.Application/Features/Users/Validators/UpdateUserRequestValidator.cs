using FluentValidation;
using ShepidiSoft.Application.Features.Users.Dtos;

namespace ShepidiSoft.Application.Features.Users.Validators;

public sealed class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
    }
}
