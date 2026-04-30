using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.StudentRequests.Queries.GetStudentRequestList;
using System.Linq.Expressions;
using ShepidiSoft.Domain.Entities; 

namespace ShepidiSoft.Application.Features.StudentRequests.Queries.GetStudentRequestList;

public sealed class GetStudentRequestListQueryHandler(
    IStudentRequestRepository studentRequestRepository,
    IMapper mapper,
    ICurrentUserService currentUserService) : IRequestHandler<GetStudentRequestListQuery, ServiceResult<List<GetStudentRequestListQueryResponse>>>
{
    public async Task<ServiceResult<List<GetStudentRequestListQueryResponse>>> Handle(GetStudentRequestListQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        var isAdmin = currentUserService.IsInRole("Admin");

        
        Expression<Func<StudentRequest, bool>> predicate = isAdmin
            ? x => true
            : x => x.StudentId == userId;

      
        var requests = await studentRequestRepository.WhereSelectAsync(
            predicate: predicate,
            selector: x => x, 
            cancellationToken: cancellationToken
        );

        
        var orderedRequests = requests
            .OrderByDescending(x => x.Created)
            .ToList();

   
        var response = mapper.Map<List<GetStudentRequestListQueryResponse>>(orderedRequests);

        return ServiceResult<List<GetStudentRequestListQueryResponse>>.Success(response);
    }
}