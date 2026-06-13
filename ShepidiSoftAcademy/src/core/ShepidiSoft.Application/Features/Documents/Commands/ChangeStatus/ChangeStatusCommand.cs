using MediatR;
using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.Application.Features.Documents.Commands.ChangeStatus;

public sealed record ChangeDocumentStatusCommand(int Id, DocumentStatus NewStatus) : IRequest<ServiceResult>;
