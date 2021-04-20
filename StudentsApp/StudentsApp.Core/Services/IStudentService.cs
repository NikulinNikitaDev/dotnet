using System.Collections.Generic;
using System.Threading.Tasks;
using StudentsApp.Core.Models;

namespace StudentsApp.Core.Services
{
    public interface IStudentService
    {
        Task<Student> CreateStudent(Student newStudent);
        Task<Student> GetStudentById(int id);
        Task<IEnumerable<Student>> GetAllStudents();
        Task UpdateStudent(int id, Student student);
        Task DeleteStudent(Student student);
    }
}