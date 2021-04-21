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
    public class DeleteStudentTests
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
            
            studentRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollection.ContainsKey(id));
            studentRepo.Setup(e => e.Remove(It.IsAny<Student>()))
                      .Callback((Student newStudent) => { dbCollection.Remove(newStudent.Id); });

            return (unitOfWork, studentRepo, dbCollection);
        }

        [Test]
        public async Task DeleteStudent_TargetItem_Success()
        {
            // Arrange
            var (unitOfWork, studentRepo, dbCollection) = GetMocks();
            var service = new StudentService(unitOfWork.Object);
            var student = new Student
            {
                Id = 5,
                Name = "Delete Group"
            };

            // Act
            await service.DeleteStudent(student);
            
            // Assert
            Assert.IsFalse(dbCollection.ContainsKey(5));
        }

        [Test]
        public void DeleteStudent_ItemDoesNotExists_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, studentRepo, dbCollection) = GetMocks();
            var service = new StudentService(unitOfWork.Object);
            var student = new Student
            {
                Id = 0,
                Name = "Delete Group"
            };

            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.DeleteStudent(student));
        }
    }
}