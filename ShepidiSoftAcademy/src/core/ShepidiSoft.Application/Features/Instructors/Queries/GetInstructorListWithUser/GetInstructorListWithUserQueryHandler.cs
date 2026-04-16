using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.Instructors.Queries.GetInstructorListWithUser;

public sealed class GetInstructorListWithUserQueryHandler(
    IInstructorRepository instructorRepository
) : IRequestHandler<
        GetInstructorListWithUserQuery,
        ServiceResult<List<GetInstructorListWithUserQueryResponse>>
    >
{
    public async Task<ServiceResult<List<GetInstructorListWithUserQueryResponse>>>
        Handle(GetInstructorListWithUserQuery request, CancellationToken cancellationToken)
    {
        var instructors = await instructorRepository.GetListAsync();

        return ServiceResult<List<GetInstructorListWithUserQueryResponse>>
            .Success(instructors);
    }
}