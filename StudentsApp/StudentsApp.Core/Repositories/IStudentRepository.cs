using System.Collections.Generic;
using System.Threading.Tasks;
using StudentsApp.Core.Models;

namespace StudentsApp.Core.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student> GetWithMarksByIdAsync(int id);
        Task<IEnumerable<Student>> GetAllWithMarksAsync();
        Task<bool> IsExists(int id);
    }
}