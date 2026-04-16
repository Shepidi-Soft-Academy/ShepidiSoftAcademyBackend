using Microsoft.AspNetCore.Http;
using ShepidiSoft.Application.Contracts.Common;
using System.Security.Claims;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid? UserId
    {
        get
        {
            var userId = httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            if (string.IsNullOrEmpty(userId)) return null;

            if (!Guid.TryParse(userId, out var parsedUserId)) return null;

            return parsedUserId;
        }
    }
}