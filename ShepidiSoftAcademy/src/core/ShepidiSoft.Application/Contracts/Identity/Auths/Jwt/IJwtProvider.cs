using System.Security.Claims;

namespace ShepidiSoft.Application.Contracts.Identity.Auths.Jwt;

public interface IJwtProvider
{
    Task<string> CreateTokenAsync(IEnumerable<Claim> claims);
}
