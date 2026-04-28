using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Domain.Entities.Enums;
using ShepidiSoft.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Persistence.StudentRequests
{
    public sealed class StudentRequestRepository(AppDbContext context) : GenericRepository<StudentRequest, Guid>(context), IStudentRequestRepository
    {

        

        public async Task<List<StudentRequest>> GetByStatusAsync(StudentRequestStatus status)
        {
            return await context.StudentRequests
            .Where(x => x.StudentRequestStatus == status)
            .ToListAsync();
            //status koda gore 
        }

        public async Task<List<StudentRequest>> GetByStudentIdAsync(Guid studentId)
        {
            return await Context.StudentRequests
                 .Where(x => x.StudentId == studentId)
                 .ToListAsync();
        }//ogrenci kendi talebini
    }
    
}
