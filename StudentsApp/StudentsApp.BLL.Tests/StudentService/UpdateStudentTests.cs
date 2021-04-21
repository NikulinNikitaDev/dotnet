using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moq;
using StudentsApp.Core;
using StudentsApp.Core.Models;
using StudentsApp.Core.Repositories;
using NUnit.Framework;

namespace StudentsApp.BLL.Tests
{
    [TestFixture]
    public class UpdateStudentTests
    {
        private static (Mock<IUnitOfWork> unitOfWork, Mock<IStudentRepository> studentRepo, Dictionary<int, Student> dbCollection) GetMocks()
        {
            var unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            var studentRepo = new Mock<IStudentRepository>(MockBehavior.Strict);
            var dbCollection = new Dictionary<int, Student>
            {
                [26] = new Student
                {
                    Id = 26,
                    Name = "Delete Group"
                },
                [27] = new Student
                {
                    Id = 27,
                    Name = "Group"
                }
            };

            unitOfWork.SetupGet(e => e.Students).Returns(studentRepo.Object);
            unitOfWork.Setup(e => e.CommitAsync()).ReturnsAsync(0);
            
            studentRepo.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollection[id]);
            studentRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollection.ContainsKey(id));

            return (unitOfWork, studentRepo, dbCollection);
        }
        
        [Test]
        public async Task UpdateStudent_FullInfo_Success()
        {
            // Arrange
            var (unitOfWork, studentRepo, dbCollection)  = GetMocks();
            var service = new StudentService(unitOfWork.Object);
            var student = new Student
            {
                Name = "New Group"
            };
        
            // Act
            await service.UpdateStudent(27, student);
            
            // Assert
            Assert.AreEqual((await unitOfWork.Object.Students.GetByIdAsync(27)).Name, student.Name);
        }
        
        [Test]
        public void UpdateStudent_EmptyName_InvalidDataException()
        {
            // Arrange
            var (unitOfWork, studentRepo, dbCollection)  = GetMocks();
            var service = new StudentService(unitOfWork.Object);
            var student = new Student()
            {
                Name = ""
            };
            
            // Act + Assert
            Assert.ThrowsAsync<InvalidDataException>(async () => await service.UpdateStudent(27, student));
        }
        
        [Test]
        public void UpdateStudent_NoItemForUpdate_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, studentRepo, dbCollection)  = GetMocks();
            var service = new StudentService(unitOfWork.Object);
            var student = new Student()
            {
                Name = "Update Group"
            };
            
            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.UpdateStudent(0, student));
        }
    }
}