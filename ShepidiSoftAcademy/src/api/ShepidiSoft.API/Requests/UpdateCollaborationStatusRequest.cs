using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.API;

public sealed record UpdateCollaborationStatusRequest(
    CollaborationApplicationStatus Status
    );
