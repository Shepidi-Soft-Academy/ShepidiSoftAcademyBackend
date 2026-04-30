using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.Application.Contracts.Persistence
{
    public interface IStudentRequestRepository:IGenericRepository<StudentRequest,Guid>
    {
        Task <List<StudentRequest>>GetByStudentIdAsync(Guid studentId);
        Task<List<StudentRequest>> GetByStatusAsync(StudentRequestStatus status);//statuye gore filtreleme icin
       
    }
}
