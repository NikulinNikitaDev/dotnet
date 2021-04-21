using System;
using System.Threading.Tasks;
using StudentsApp.Core.Repositories;

namespace StudentsApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IMarkRepository Marks { get; }
        IStudentRepository Students { get; }
        Task<int> CommitAsync();
    }
}