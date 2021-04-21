using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StudentsApp.Core.Models
{
    public class Student
    {
        public Student()
        {
            Marks = new Collection<Mark>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Mark> Marks { get; set; }
    }
}