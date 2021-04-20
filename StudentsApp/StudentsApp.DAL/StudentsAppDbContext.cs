using Microsoft.EntityFrameworkCore;
using StudentsApp.Core.Models;
using StudentsApp.DAL.Configurations;

namespace StudentsApp.DAL
{
    public class StudentsAppDbContext : DbContext
    {
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Student> Students { get; set; }

        public StudentsAppDbContext(DbContextOptions<StudentsAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MarkConfiguration());
            builder.ApplyConfiguration(new StudentConfiguration());
        }
    }
}