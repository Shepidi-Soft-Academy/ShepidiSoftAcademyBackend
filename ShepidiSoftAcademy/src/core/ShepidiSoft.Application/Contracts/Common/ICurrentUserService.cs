namespace ShepidiSoft.Application.Contracts.Common;

public interface ICurrentUserService
{
    Guid? UserId { get; }
}