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
    public class GetMarkByIdTests
    {
        private static (Mock<IUnitOfWork> unitOfWork, Mock<IMarkRepository> markRepo, Dictionary<int, Mark> dbCollectionMark) GetMocks()
        {
            var unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            var markRepo = new Mock<IMarkRepository>(MockBehavior.Strict);
            var studentRepo = new Mock<IStudentRepository>(MockBehavior.Strict);
            var dbCollectionMark = new Dictionary<int, Mark>
            {
                [26] = new Mark
                {
                    Id = 26,
                    StudentId = 26,
                    Grade = 2
                },
                [27] = new Mark
                {
                    Id = 27,
                    StudentId = 27,
                    Grade = 2
                }
            };
            
            var dbCollectionStudents = new Dictionary<int, Student>
            {
                [26] = new Student
                {
                    Id = 26,
                    Name = "Teacher"
                },
                [27] = new Student
                {
                    Id = 27,
                    Name = "Other teacher"
                }
            };

            unitOfWork.SetupGet(e => e.Marks).Returns(markRepo.Object);
            unitOfWork.SetupGet(e => e.Students).Returns(studentRepo.Object);
            unitOfWork.Setup(e => e.CommitAsync()).ReturnsAsync(0);
            
            markRepo.Setup(e => e.GetWithStudentByIdAsync(It.IsAny<int>()))
                     .ReturnsAsync((int id) => dbCollectionMark[id]);
            
            studentRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionStudents.ContainsKey(id));

            return (unitOfWork, markRepo, dbCollectionMark);
        }
        
        [Test]
        public async Task GetMarkById_ItemExists_Success()
        {
            // Arrange
            var (unitOfWork, markRepo, dbCollectionMark) = GetMocks();
            var service = new MarkService(unitOfWork.Object);

            // Act
            var mark = await service.GetMarkById(27);
            
            // Assert
            Assert.AreEqual(mark, dbCollectionMark[27]);
        }
        
        [Test]
        public void GetMarkById_ItemDoesNotExists_KeyNotFoundException()
        {
            // Arrange
            var (unitOfWork, markRepo, dbCollectionMark) = GetMocks();
            var service = new MarkService(unitOfWork.Object);

            // Act + Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.GetMarkById(0));
        }
    }
}