using MediatR;
using ShepidiSoft.Application;

public sealed record UpdateActivityCommand(
    int Id,
    string Title,
    string Description,
    DateTime Date,
    bool IsOnline,
    string? Location,
    string? OnlineMeetingUrl
) : IRequest<ServiceResult>;
