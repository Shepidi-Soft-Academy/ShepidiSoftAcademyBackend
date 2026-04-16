namespace ShepidiSoft.Application.Features.Users.Dtos;

// Dto
public sealed record UserDto(Guid Id, string FirstName, string LastName, string Email,string? PhotoUrl,string LinkednUrl);