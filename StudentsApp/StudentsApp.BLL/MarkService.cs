using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using StudentsApp.Core;
using StudentsApp.Core.Models;
using StudentsApp.Core.Services;

namespace StudentsApp.BLL
{
    public class MarkService : IMarkService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public MarkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Mark> CreateMark(Mark newMark)
        {
            if (newMark is null)
                throw new NullReferenceException();
            
            await _unitOfWork.Marks.AddAsync(newMark);
            await _unitOfWork.CommitAsync();

            return newMark;
        }

        public async Task<Mark> GetMarkById(int id)
        {
            return await _unitOfWork.Marks.GetWithStudentByIdAsync(id);
        }

        public async Task<IEnumerable<Mark>> GetMarksByStudentId(int studentId)
        {
            return await _unitOfWork.Marks.GetAllWithStudentByStudentIdAsync(studentId);
        }

        public async Task<IEnumerable<Mark>> GetAllWithStudent()
        {
            return await _unitOfWork.Marks.GetAllWithStudentAsync();
        }

        public async Task UpdateMark(int id, Mark mark)
        {
            if (!await _unitOfWork.Marks.IsExists(id))
                throw new NullReferenceException();
            
            if (mark.Grade < 2 || mark.Grade > 5 || mark.StudentId <= 0)
                throw new InvalidDataException();
            
            var markToBeUpdated = await GetMarkById(id);
            markToBeUpdated.Grade = mark.Grade;
            markToBeUpdated.StudentId = mark.StudentId;

            await _unitOfWork.CommitAsync();
        }
        
        public async Task DeleteMark(Mark mark)
        {
            if (!(await _unitOfWork.Marks.IsExists(mark.Id)))
                throw new NullReferenceException();
            
            _unitOfWork.Marks.Remove(mark);
            
            await _unitOfWork.CommitAsync();
        }
    }
}