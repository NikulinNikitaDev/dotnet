using System.Collections.Generic;
using System.Threading.Tasks;
using StudentsApp.Core.Models;

namespace StudentsApp.Core.Services
{
    public interface IMarkService
    {
        Task<Mark> CreateMark(Mark newMark);
        Task<Mark> GetMarkById(int id);
        Task<IEnumerable<Mark>> GetAllWithStudent();
        Task<IEnumerable<Mark>> GetMarksByStudentId(int studentId);
        Task UpdateMark(int id, Mark mark);
        Task DeleteMark(Mark mark);
    }
}