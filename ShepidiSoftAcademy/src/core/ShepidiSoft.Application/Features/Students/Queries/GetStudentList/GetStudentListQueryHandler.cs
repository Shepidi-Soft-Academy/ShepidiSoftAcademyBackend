using MediatR;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.Students.Queries.GetStudentList
{
    public sealed class GetStudentListQueryHandler(
     IStudentRepository studentRepository,
     IUserService userService
 ) : IRequestHandler<GetStudentListQuery, ServiceResult<List<GetStudentListQueryResponse>>>
    {
        public async Task<ServiceResult<List<GetStudentListQueryResponse>>> Handle(
            GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var students = await studentRepository.GetAllAsync();

            if (!students.Any())
                return ServiceResult<List<GetStudentListQueryResponse>>.Success([]);

            var userIds = students.Select(s => s.UserId).ToList();
            var usersResult = await userService.GetUsersByIdsAsync(userIds, cancellationToken);

            if (!usersResult.IsSuccess)
                return ServiceResult<List<GetStudentListQueryResponse>>.Fail(usersResult.ErrorMessage!);

            var response = students.Select(s =>
            {
                var user = usersResult.Data!.FirstOrDefault(u => u.Id == s.UserId);
                return new GetStudentListQueryResponse(
                    Id: s.Id,
                    Name: user?.FirstName ?? string.Empty,
                    Surname: user?.LastName ?? string.Empty
              
                );
            }).ToList();

            return ServiceResult<List<GetStudentListQueryResponse>>.Success(response);
        }
    }
}
