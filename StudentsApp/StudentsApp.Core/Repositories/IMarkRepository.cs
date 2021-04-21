using System.Collections.Generic;
using System.Threading.Tasks;
using StudentsApp.Core.Models;

namespace StudentsApp.Core.Repositories
{
    public interface IMarkRepository : IRepository<Mark>
    {
        Task<Mark> GetWithStudentByIdAsync(int id);
        Task<IEnumerable<Mark>> GetAllWithStudentAsync();
        Task<IEnumerable<Mark>> GetAllWithStudentByStudentIdAsync(int studentId);
        Task<bool> IsExists(int id);
    }
}