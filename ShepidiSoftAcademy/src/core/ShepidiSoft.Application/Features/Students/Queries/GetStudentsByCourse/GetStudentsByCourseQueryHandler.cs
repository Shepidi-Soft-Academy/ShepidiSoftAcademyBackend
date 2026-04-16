using System.Net;
using AutoMapper;
using MediatR;
using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;

public sealed class GetStudentsByCourseQueryHandler(
   ICourseRepository courseRepository,
   IStudentRepository studentRepository,
   IUserService userService


) : IRequestHandler<GetStudentsByCourseQuery, ServiceResult<List<GetStudentsByCourseQueryResponse>>>
{
    public async Task<ServiceResult<List<GetStudentsByCourseQueryResponse>>> Handle(
    GetStudentsByCourseQuery request, CancellationToken cancellationToken)
{
    var course = await courseRepository.GetByIdAsync(request.CourseId);

    if (course is null)
        return ServiceResult<List<GetStudentsByCourseQueryResponse>>.Fail(
            "Kurs bulunamadı", HttpStatusCode.NotFound);

    var students = await studentRepository.GetStudentsByCourseIdAsync(
        request.CourseId, cancellationToken);

    if (!students.Any())
        return ServiceResult<List<GetStudentsByCourseQueryResponse>>.Success([]);

    var userIds = students.Select(s => s.UserId).ToList();
    var usersResult = await userService.GetUsersByIdsAsync(userIds, cancellationToken);

    if (!usersResult.IsSuccess)
        return ServiceResult<List<GetStudentsByCourseQueryResponse>>.Fail(usersResult.ErrorMessage!);

    var response = students.Select(s =>
    {
        var user = usersResult.Data!.FirstOrDefault(u => u.Id == s.UserId);
        return new GetStudentsByCourseQueryResponse(
            UserId:user!.Id,
            Id: s.Id,
            Name: user?.FirstName ?? string.Empty,
            Surname: user?.LastName ?? string.Empty,
            Mail: user?.Email ?? string.Empty
        );
    }).ToList();

    return ServiceResult<List<GetStudentsByCourseQueryResponse>>.Success(response);
}
}