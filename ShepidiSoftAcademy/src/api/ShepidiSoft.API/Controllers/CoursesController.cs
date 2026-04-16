using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Courses.Commands.CreateCourse;
using ShepidiSoft.Application.Features.Courses.Commands.DeleteCourse;
using ShepidiSoft.Application.Features.Courses.Commands.UpdateCourse;
using ShepidiSoft.Application.Features.Courses.Queries.GetCourseDetail;
using ShepidiSoft.Application.Features.Courses.Queries.GetCourses;
using ShepidiSoft.Application.Features.Courses.Queries.GetCoursesAdmin;
using ShepidiSoft.Application.Features.Courses.Queries.GetMyCourses;
using ShepidiSoft.Application.Features.Students.Commands.AssignStudentToCourse;
using ShepidiSoft.Application.Features.Students.Commands.RemoveStudentFromCourse;

namespace ShepidiSoft.API.Controllers;


public sealed class CoursesController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> Create(
CreateCourseCommand request,
CancellationToken cancellationToken)
=> CreateActionResult(await _mediator.Send(request, cancellationToken));

    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteCourseCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken));
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> Update(int id, UpdateCourseRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCourseCommand(
            Id: id,
            Title: request.Title,
            Description: request.Description,
            InstructorId: request.InstructorId,
            IsOnline: request.IsOnline,
            Location: request.Location,
            MeetingUrl:request.MeetingUrl,
            CoverImageUrl:request.CoverImageUrl,
            StartedDate:request.StartedDate,
            EndDate:request.EndDate,
            Status:request.Status,
            Price:request.Price,
            DurationInWeeks:request.DurationInWeeks
            );

        var result = await _mediator.Send(command, cancellationToken);
        return CreateActionResult(result);
    }

    [HttpGet("MyCourses")]

    [AllowAnonymous]

    public async Task<IActionResult> GetMyCourses(CancellationToken cancellationToken)
 => CreateActionResult(
     await _mediator.Send(new GetMyCoursesQuery(), cancellationToken));

    [HttpGet]

    [AllowAnonymous]

    public async Task<IActionResult> GetCourses(CancellationToken cancellationToken)
=> CreateActionResult(
 await _mediator.Send(new GetCoursesQuery(), cancellationToken));

    [HttpGet("{id}")]
    [AllowAnonymous]

    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
=> CreateActionResult(
    await _mediator.Send(new GetCourseDetailQuery(id), cancellationToken));

    [HttpGet("{courseId}/students")]
public async Task<IActionResult> GetStudentsByCourse(int courseId, CancellationToken cancellationToken)
    => CreateActionResult(await _mediator.Send(new GetStudentsByCourseQuery(courseId), cancellationToken));

    [HttpPost("{courseId}/students")]
    public async Task<IActionResult> AssignStudent(int courseId, Guid studentId, CancellationToken cancellationToken)
    => CreateActionResult(await _mediator.Send(new AssignStudentToCourseCommand(studentId, courseId), cancellationToken));

    [HttpDelete("{courseId}/students/{userId}")]
    public async Task<IActionResult> RemoveStudent(int courseId, Guid userId, CancellationToken cancellationToken)
    => CreateActionResult(await _mediator.Send(new RemoveStudentFromCourseCommand(courseId,userId), cancellationToken));
    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetCoursesAdmin(CancellationToken cancellationToken)
    => CreateActionResult(
        await _mediator.Send(new GetCoursesAdminQuery(), cancellationToken));

}
