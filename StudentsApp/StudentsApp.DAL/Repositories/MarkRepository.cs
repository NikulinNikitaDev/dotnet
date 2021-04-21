using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentsApp.Core.Models;
using StudentsApp.Core.Repositories;

namespace StudentsApp.DAL.Repositories
{
    public class MarkRepository : Repository<Mark>, IMarkRepository
    {
        public MarkRepository(StudentsAppDbContext context) : base(context) { }

        public async Task<Mark> GetWithStudentByIdAsync(int id)
        {
            return await MyMarkDbContext.Marks.Include(m => m.Student)
                                                .SingleOrDefaultAsync(m => m.Id == id);;
        }
        
        public async Task<IEnumerable<Mark>> GetAllWithStudentAsync()
        {
            return await MyMarkDbContext.Marks.Include(m => m.Student)
                                                .ToListAsync();
        }
        

        public async Task<IEnumerable<Mark>> GetAllWithStudentByStudentIdAsync(int studentId)
        {
            return await MyMarkDbContext.Marks.Include(m => m.Student)
                                                .Where(m => m.StudentId == studentId)
                                                .ToListAsync();
        }
        
        public async Task<bool> IsExists(int id)
        {
            return await GetByIdAsync(id) is {};
        }
        
        private StudentsAppDbContext MyMarkDbContext => Context as StudentsAppDbContext;
    }
}