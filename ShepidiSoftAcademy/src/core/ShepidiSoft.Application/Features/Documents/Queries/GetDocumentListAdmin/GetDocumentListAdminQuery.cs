using MediatR;

namespace ShepidiSoft.Application.Features.Documents.Queries.GetDocumentListAdmin;

public sealed record GetDocumentListAdminQuery() : IRequest<ServiceResult<List<GetDocumentListAdminQueryResponse>>>;
