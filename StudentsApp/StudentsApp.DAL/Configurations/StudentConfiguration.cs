using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsApp.Core.Models;

namespace StudentsApp.DAL.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            const int maxLength = 50;
            
            builder.HasKey(a => a.Id);

            builder.Property(m => m.Id)
                   .UseIdentityColumn();
                
            builder.Property(m => m.Name)
                   .IsRequired()
                   .HasMaxLength(maxLength);

            builder.ToTable("Students");
        }
    }
}