namespace ShepidiSoft.Application.Features.Users.Dtos;

public sealed record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth,
    string LinkednUrl,
    string PhotoUrl,
    string GithubUrl,
    string YoutubeUrl);
