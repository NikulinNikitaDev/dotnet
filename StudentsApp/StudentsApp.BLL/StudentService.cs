using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using StudentsApp.Core;
using StudentsApp.Core.Models;
using StudentsApp.Core.Services;

namespace StudentsApp.BLL
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Student> CreateStudent(Student newStudent)
        {
            if (newStudent is null)
                throw new NullReferenceException();
            
            await _unitOfWork.Students.AddAsync(newStudent);
            await _unitOfWork.CommitAsync();
            
            return newStudent;
        }
        
        public async Task<Student> GetStudentById(int id)
        {
            return await _unitOfWork.Students.GetByIdAsync(id);
        }
        
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _unitOfWork.Students.GetAllAsync();
        }

        public async Task UpdateStudent(int id, Student student)
        {
            if (!await _unitOfWork.Students.IsExists(id))
                throw new NullReferenceException();
            
            if (student.Name.Length == 0 || student.Name.Length > 50)
                throw new InvalidDataException();

            var studentToBeUpdated = await GetStudentById(id);
            studentToBeUpdated.Name = student.Name;
            
            await _unitOfWork.CommitAsync();
        }
        
        public async Task DeleteStudent(Student student)
        {
            if (!await _unitOfWork.Students.IsExists(student.Id))
                throw new NullReferenceException();
            
            _unitOfWork.Students.Remove(student);
            
            await _unitOfWork.CommitAsync();
        }
    }
}