using System.Threading.Tasks;
using StudentsApp.Core;
using StudentsApp.Core.Repositories;
using StudentsApp.DAL.Repositories;

namespace StudentsApp.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentsAppDbContext _context;
        private MarkRepository _markRepository;
        private StudentRepository _studentRepository;

        public UnitOfWork(StudentsAppDbContext context)
        {
            _context = context;
        }

        public IMarkRepository Marks => _markRepository ??= new MarkRepository(_context);

        public IStudentRepository Students => _studentRepository ??= new StudentRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}