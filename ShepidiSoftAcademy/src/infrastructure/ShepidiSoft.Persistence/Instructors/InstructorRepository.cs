using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Instructors.Queries.GetInstructorDetail;
using ShepidiSoft.Application.Features.Instructors.Queries.GetInstructorListWithUser;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.Instructors;

public sealed class InstructorRepository(AppDbContext context) : GenericRepository<Instructor, int>(context), IInstructorRepository
{
    public async Task<ServiceResult<bool>> CheckIsInstructorExistByUserId(Guid userId)
    {
        var exists = await context.Instructors.AnyAsync(i => i.UserId == userId);
        return ServiceResult<bool>.Success(exists);
    }

    public async  Task<GetInstructorDetailWithUserQueryResponse?> GetInstructorDetailAsync(int id)
    {
        return await (
            from instructor in context.Instructors
            join user in context.Users
                on instructor.UserId equals user.Id
            where instructor.Id == id
            select new GetInstructorDetailWithUserQueryResponse(
                instructor.Id,
                user.FirstName + " " + user.LastName,
                instructor.Expertise,
                instructor.IsActive.ToString(),
                instructor.Bio,
                user.PhotoUrl,
                user.Email,
                user.GithubUrl,
                user.LinkednUrl,
                user.YoutubeUrl
            )
        ).FirstOrDefaultAsync();
    }

    public async Task<ServiceResult<int>> GetInstructorIdByUserId(Guid userId)
    {
        var instructor = await context.Instructors
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.UserId == userId);

        if (instructor == null)
            return ServiceResult<int>.Fail("Eğitmen bulunamadı.");

        return ServiceResult<int>.Success(instructor.Id);
    }

    public async Task<List<GetInstructorListWithUserQueryResponse>> GetListAsync()
    {
        return await (
            from instructor in context.Instructors
            join user in context.Users
                on instructor.UserId equals user.Id
            select new GetInstructorListWithUserQueryResponse(
                instructor.Id,
                user.FirstName + " " + user.LastName,
                instructor.Expertise,
                instructor.IsActive.ToString(),
                user.GithubUrl,
                user.LinkednUrl,
                user.YoutubeUrl,
                user.PhotoUrl
            )
        ).ToListAsync();
    }

    public async Task<Guid?> GetUserIdByInstructorIdAsync(int instructorId, CancellationToken cancellationToken)
    {
        return await context.Instructors
            .Where(i => i.Id == instructorId)
            .Select(i => (Guid?)i.UserId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
