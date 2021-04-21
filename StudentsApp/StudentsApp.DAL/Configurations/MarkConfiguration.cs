using StudentsApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentsApp.DAL.Configurations
{
    public class MarkConfiguration : IEntityTypeConfiguration<Mark>
    {
        public void Configure(EntityTypeBuilder<Mark> builder)
        {
            const int maxLength = 50;
            
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                   .UseIdentityColumn();
                
            builder.Property(m => m.Grade)
                   .IsRequired()
                   .HasMaxLength(maxLength);

            builder.HasOne(m => m.Student)
                   .WithMany(a => a.Marks)
                   .HasForeignKey(m => m.StudentId);

            builder.ToTable("Marks");
        }
    }
}