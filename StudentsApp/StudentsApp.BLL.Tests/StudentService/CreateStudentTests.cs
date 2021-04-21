using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using StudentsApp.Core;
using StudentsApp.Core.Models;
using StudentsApp.Core.Repositories;
using NUnit.Framework;

namespace StudentsApp.BLL.Tests
{
    [TestFixture]
    public class CreateStudentTests
    {
        private static (Mock<IUnitOfWork> unitOfWork, Mock<IStudentRepository> studentRepo, Dictionary<int, Student> dbCollection) GetMocks()
        {
            var unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            var studentRepo = new Mock<IStudentRepository>(MockBehavior.Strict);
            var dbCollection = new Dictionary<int, Student>
            {
                [5] = new Student
                {
                    Id = 5,
                    Name = "Delete Group"
                },
                [6] = new Student
                {
                    Id = 6,
                    Name = "Group"
                }
            };

            unitOfWork.SetupGet(e => e.Students).Returns(studentRepo.Object);
            unitOfWork.Setup(e => e.CommitAsync()).ReturnsAsync(0);
            
            studentRepo.Setup(e => e.AddAsync(It.IsAny<Student>()))
                      .Callback((Student newStudent) => { dbCollection.Add(newStudent.Id, newStudent); })
                      .Returns((Student _) => Task.CompletedTask);

            return (unitOfWork, studentRepo, dbCollection);
        }
        
        [Test]
        public async Task CreateStudent_FullInfo_Success()
        {
            // Arrange
            var (unitOfWork, studentRepo, dbCollection) = GetMocks();
            var service = new StudentService(unitOfWork.Object);
            var student = new Student
            {
                Id = 7,
                Name = "New Group"
            };

            // Act
            await service.CreateStudent(student);

            // Assert
            Assert.IsTrue(dbCollection.ContainsKey(student.Id));
        }
        
        [Test]
        public void CreateStudent_NullObject_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, studentRepo, dbCollection) = GetMocks();
            var service = new StudentService(unitOfWork.Object);

            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.CreateStudent(null));
        }
    }
}