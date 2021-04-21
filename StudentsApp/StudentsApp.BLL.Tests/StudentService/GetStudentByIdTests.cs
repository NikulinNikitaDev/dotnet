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
    public class GetStudentByIdTests
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
            
            studentRepo.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollection[id]);

            return (unitOfWork, studentRepo, dbCollection);
        }
        
        [Test]
        public async Task GetStudentById_ItemExists_Success()
        {
            // Arrange
            var (unitOfWork, studentRepo, dbCollection) = GetMocks();
            var service = new StudentService(unitOfWork.Object);

            // Act
            var student = await service.GetStudentById(6);
            
            // Assert
            Assert.AreEqual(student, dbCollection[6]);
        }
        
        [Test]
        public void GetStudentById_ItemDoesNotExists_KeyNotFoundException()
        {
            // Arrange
            var (unitOfWork, studentRepo, dbCollection) = GetMocks();
            var service = new StudentService(unitOfWork.Object);

            // Act + Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.GetStudentById(0));
        }
    }
}