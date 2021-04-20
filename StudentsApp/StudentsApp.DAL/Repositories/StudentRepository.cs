using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentsApp.Core.Models;
using StudentsApp.Core.Repositories;

namespace StudentsApp.DAL.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(StudentsAppDbContext context) : base(context) { }
        
        public async Task<Student> GetWithMarksByIdAsync(int id)
        {
            return await MyMarkDbContext.Students.Include(a => a.Marks)
                                                 .SingleOrDefaultAsync(a => a.Id == id);
        }
        
        public async Task<IEnumerable<Student>> GetAllWithMarksAsync()
        {
            return await MyMarkDbContext.Students.Include(a => a.Marks)
                                                 .ToListAsync();
        }
        
        public async Task<bool> IsExists(int id)
        {
            return await GetByIdAsync(id) is {};
        }

        private StudentsAppDbContext MyMarkDbContext => Context as StudentsAppDbContext;
    }
}