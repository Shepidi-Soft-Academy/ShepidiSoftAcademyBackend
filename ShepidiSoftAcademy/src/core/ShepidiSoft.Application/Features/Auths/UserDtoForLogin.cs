namespace ShepidiSoft.Application.Features.Auths;

public record UserDtoForLogin(Guid UserId, string Mail, string UserName, string Refreshtoken, DateTime RefreshtokenExpires);
