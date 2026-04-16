using MediatR;
using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Instructors.Queries.GetInstructorDetail;

public sealed class GetInstructorDetailWithUserQueryHandler(
    IInstructorRepository instructorRepository
) : IRequestHandler<GetInstructorDetailWithUserQuery, ServiceResult<GetInstructorDetailWithUserQueryResponse>>
{
    public async Task<ServiceResult<GetInstructorDetailWithUserQueryResponse>> Handle(
        GetInstructorDetailWithUserQuery request,
        CancellationToken cancellationToken)
    {
        var instructor = await instructorRepository.GetInstructorDetailAsync(request.Id);

        if (instructor is null)
        {
            return ServiceResult<GetInstructorDetailWithUserQueryResponse>.Fail(
                "Eğitmen bulunamadı."
            );
        }

        return ServiceResult<GetInstructorDetailWithUserQueryResponse>.Success(instructor);
    }
}