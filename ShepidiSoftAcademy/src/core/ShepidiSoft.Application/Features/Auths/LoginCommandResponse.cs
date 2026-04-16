namespace ShepidiSoft.Application.Features.Auths;

public record LoginCommandResponse(string AccessToken, string RefreshToken, DateTime RefreshTokenExpires);