using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Courses.Commands.CreateCourse;

public sealed class CreateCourseCommandHandler(
    ICourseRepository courseRepository,
    IInstructorRepository instructorRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICourseMembershipRepository courseMembershipRepository
) : IRequestHandler<CreateCourseCommand, ServiceResult<CreateCourseCommandResponse>>
{
    private readonly ICourseRepository _courseRepository = courseRepository;
    private readonly IInstructorRepository _instructorRepository = instructorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICourseMembershipRepository _courseMembershipRepository = courseMembershipRepository;

    public async Task<ServiceResult<CreateCourseCommandResponse>> Handle(
        CreateCourseCommand request,
        CancellationToken cancellationToken)
    {
        var instructorExists = await _instructorRepository
            .AnyAsync(x => x.Id == request.InstructorId);

        if (!instructorExists)
            return ServiceResult<CreateCourseCommandResponse>
                .Fail("Eğitmen bulunamadı.");

        var instructorUserId = await _instructorRepository
            .GetUserIdByInstructorIdAsync(request.InstructorId, cancellationToken);

        if (instructorUserId == null)
            return ServiceResult<CreateCourseCommandResponse>.Fail("Instructor UserId bulunamadı.");

        var course = _mapper.Map<Course>(request);

        await _courseRepository.AddAsync(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _courseMembershipRepository.AddInstructorToCourseAsync(
            course.Id,
            instructorUserId.Value,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<CreateCourseCommandResponse>
            .Success(new CreateCourseCommandResponse(course.Id));
    }
}