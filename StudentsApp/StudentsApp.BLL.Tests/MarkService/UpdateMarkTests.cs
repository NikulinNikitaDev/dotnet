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
    public class UpdateMarkTests
    {
        private static (Mock<IUnitOfWork> unitOfWork, Mock<IMarkRepository> markRepo, Dictionary<int, Mark> dbCollectionMark) GetMocks()
        {
            var unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            var markRepo = new Mock<IMarkRepository>(MockBehavior.Strict);
            var studentRepo = new Mock<IStudentRepository>(MockBehavior.Strict);
            var dbCollectionMark = new Dictionary<int, Mark>
            {
                [5] = new Mark
                {
                    Id = 5,
                    StudentId = 5,
                    Grade = 2
                },
                [6] = new Mark
                {
                    Id = 6,
                    StudentId = 6,
                    Grade = 2
                }
            };
            
            var dbCollectionStudents = new Dictionary<int, Student>
            {
                [5] = new Student
                {
                    Id = 5,
                    Name = "Teacher"
                },
                [6] = new Student
                {
                    Id = 6,
                    Name = "Other teacher"
                }
            };

            unitOfWork.SetupGet(e => e.Marks).Returns(markRepo.Object);
            unitOfWork.SetupGet(e => e.Students).Returns(studentRepo.Object);
            unitOfWork.Setup(e => e.CommitAsync()).ReturnsAsync(0);
            
            markRepo.Setup(e => e.GetWithStudentByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionMark[id]);
            markRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionMark.ContainsKey(id));
            
            studentRepo.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionStudents[id]);
            studentRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionStudents.ContainsKey(id));

            return (unitOfWork, markRepo, dbCollectionMark);
        }
        
        [Test]
        public async Task UpdateMark_FullInfo_Success()
        {
            // Arrange
            var (unitOfWork, markRepo, dbCollectionMark)  = GetMocks();
            var service = new MarkService(unitOfWork.Object);
            var mark = new Mark
            {
                StudentId = 6,
                Grade = 2
            };
        
            // Act
            await service.UpdateMark(6, mark);
            
            // Assert
            Assert.AreEqual((await unitOfWork.Object.Marks.GetWithStudentByIdAsync(6)).Grade, mark.Grade);
        }
        
        [Test]
        public void UpdateMark_EmptyName_InvalidDataException()
        {
            // Arrange
            var (unitOfWork, markRepo, dbCollectionMark)  = GetMocks();
            var service = new MarkService(unitOfWork.Object);
            var mark = new Mark()
            {
                Grade = 2
            };
            
            // Act + Assert
            Assert.ThrowsAsync<InvalidDataException>(async () => await service.UpdateMark(6, mark));
        }
        
        [Test]
        public void UpdateMark_NoItemForUpdate_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, markRepo, dbCollectionMark)  = GetMocks();
            var service = new MarkService(unitOfWork.Object);
            var mark = new Mark()
            {
                Grade = 2
            };
            
            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.UpdateMark(0, mark));
        }
    }
}