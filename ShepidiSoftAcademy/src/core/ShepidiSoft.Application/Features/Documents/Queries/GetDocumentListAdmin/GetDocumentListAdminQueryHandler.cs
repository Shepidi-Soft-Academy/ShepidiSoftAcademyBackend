using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.Documents.Queries.GetDocumentListAdmin;

public sealed class GetDocumentListAdminQueryHandler(
    IDocumentRepository documentRepository,
    IMapper mapper,
    IUserService userService
    ) : IRequestHandler<GetDocumentListAdminQuery, ServiceResult<List<GetDocumentListAdminQueryResponse>>>
{
    public async Task<ServiceResult<List<GetDocumentListAdminQueryResponse>>> Handle(GetDocumentListAdminQuery request, CancellationToken cancellationToken)
    {
        // Tüm dokümanları getir
        var documents = await documentRepository.GetAllAsync();

        if (!documents.Any())
            return ServiceResult<List<GetDocumentListAdminQueryResponse>>.Success([]);

        // CreatedBy ID'lerini topla
        var creatorIds = documents
            .Where(d => d.CreatedBy.HasValue)
            .Select(d => d.CreatedBy.Value)
            .Distinct()
            .ToList();

        // Kullanıcı bilgilerini getir
        var usersResult = await userService.GetUsersByIdsAsync(creatorIds, cancellationToken);

        if (!usersResult.IsSuccess)
            return ServiceResult<List<GetDocumentListAdminQueryResponse>>.Fail(usersResult.ErrorMessage!);

        // Response oluştur
        var response = documents.Select(d =>
        {
            var creator = usersResult.Data?.FirstOrDefault(u => u.Id == d.CreatedBy);
            var createdByEmail = creator?.Email ?? "Bilinmiyor";

            return new GetDocumentListAdminQueryResponse(
                Id: d.Id,
                DocumentTopicName: d.DocumentTopic?.Name ?? "Kategorisiz",
                Title: d.Title,
                Description: d.Description,
                FileUrl: d.FileUrl,
                CreatedByMail: createdByEmail,
                CreatedTime: d.Created
            );
        }).ToList();

        return ServiceResult<List<GetDocumentListAdminQueryResponse>>.Success(response);
    }
}
